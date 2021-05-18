namespace Micube.SmartMES.ProcessManagement
{
	partial class ReworkRoutingPopup
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnOk = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtReworkName = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtReworkNumber = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdProcessDef = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdProcessPath = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkNumber.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(791, 563);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 539);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(791, 24);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(716, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnOk
            // 
            this.btnOk.AllowFocus = false;
            this.btnOk.IsBusy = false;
            this.btnOk.IsWrite = false;
            this.btnOk.LanguageKey = "OK";
            this.btnOk.Location = new System.Drawing.Point(638, 0);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "확인";
            this.btnOk.TooltipLanguageKey = "";
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.txtReworkName);
            this.smartPanel1.Controls.Add(this.smartLabel2);
            this.smartPanel1.Controls.Add(this.txtReworkNumber);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(791, 25);
            this.smartPanel1.TabIndex = 1;
            // 
            // txtReworkName
            // 
            this.txtReworkName.LabelText = null;
            this.txtReworkName.LanguageKey = null;
            this.txtReworkName.Location = new System.Drawing.Point(395, 3);
            this.txtReworkName.Name = "txtReworkName";
            this.txtReworkName.Size = new System.Drawing.Size(293, 20);
            this.txtReworkName.TabIndex = 7;
            // 
            // smartLabel2
            // 
            this.smartLabel2.LanguageKey = "REWORKNAME";
            this.smartLabel2.Location = new System.Drawing.Point(327, 6);
            this.smartLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(40, 14);
            this.smartLabel2.TabIndex = 6;
            this.smartLabel2.Text = "재작업명";
            // 
            // txtReworkNumber
            // 
            this.txtReworkNumber.LabelText = null;
            this.txtReworkNumber.LanguageKey = null;
            this.txtReworkNumber.Location = new System.Drawing.Point(78, 3);
            this.txtReworkNumber.Name = "txtReworkNumber";
            this.txtReworkNumber.Size = new System.Drawing.Size(226, 20);
            this.txtReworkNumber.TabIndex = 7;
            // 
            // smartLabel1
            // 
            this.smartLabel1.LanguageKey = "REWORKNUMBER";
            this.smartLabel1.Location = new System.Drawing.Point(5, 6);
            this.smartLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(50, 14);
            this.smartLabel1.TabIndex = 6;
            this.smartLabel1.Text = "재작업번호";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(706, 3);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(85, 20);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "검색";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterContainer1, 0, 2);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 5;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(791, 563);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 30);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdProcessDef);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdProcessPath);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(791, 504);
            this.smartSpliterContainer1.SplitterPosition = 221;
            this.smartSpliterContainer1.TabIndex = 4;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdProcessDef
            // 
            this.grdProcessDef.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessDef.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessDef.IsUsePaging = false;
            this.grdProcessDef.LanguageKey = "ROUTINGLIST";
            this.grdProcessDef.Location = new System.Drawing.Point(0, 0);
            this.grdProcessDef.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.grdProcessDef.Name = "grdProcessDef";
            this.grdProcessDef.ShowBorder = true;
            this.grdProcessDef.Size = new System.Drawing.Size(791, 221);
            this.grdProcessDef.TabIndex = 1;
            this.grdProcessDef.UseAutoBestFitColumns = false;
            // 
            // grdProcessPath
            // 
            this.grdProcessPath.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessPath.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessPath.IsUsePaging = false;
            this.grdProcessPath.LanguageKey = "PROCESSSEGMENT";
            this.grdProcessPath.Location = new System.Drawing.Point(0, 0);
            this.grdProcessPath.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.grdProcessPath.Name = "grdProcessPath";
            this.grdProcessPath.ShowBorder = true;
            this.grdProcessPath.Size = new System.Drawing.Size(791, 278);
            this.grdProcessPath.TabIndex = 1;
            this.grdProcessPath.UseAutoBestFitColumns = false;
            // 
            // ReworkRoutingPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 583);
            this.LanguageKey = "REWORKROUTING";
            this.Name = "ReworkRoutingPopup";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkNumber.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnOk;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdProcessDef;
        private Framework.SmartControls.SmartBandedGrid grdProcessPath;
        private Framework.SmartControls.SmartTextBox txtReworkName;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartTextBox txtReworkNumber;
        private Framework.SmartControls.SmartLabel smartLabel1;
    }
}
