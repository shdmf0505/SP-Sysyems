namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingCostClosing
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
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tabClose = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapExpense = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdCostClosing = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtperiod = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblPeriod = new Micube.Framework.SmartControls.SmartLabel();
            this.tapMajor = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdCostClosingMajor = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel3 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSettlemantProcess = new Micube.Framework.SmartControls.SmartButton();
            this.btnSettlemantList = new Micube.Framework.SmartControls.SmartButton();
            this.txtperiodMajor = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.btnCloseSend = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnAggregate = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabClose)).BeginInit();
            this.tabClose.SuspendLayout();
            this.tapExpense.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiod.Properties)).BeginInit();
            this.tapMajor.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).BeginInit();
            this.smartPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiodMajor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 900);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnCloseSend);
            this.pnlToolbar.Controls.Add(this.btnAggregate);
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Size = new System.Drawing.Size(843, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnAggregate, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnCloseSend, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tplMain);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.tabClose, 0, 0);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 1;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 472F));
            this.tplMain.Size = new System.Drawing.Size(843, 903);
            this.tplMain.TabIndex = 0;
            // 
            // tabClose
            // 
            this.tabClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabClose.Location = new System.Drawing.Point(3, 3);
            this.tabClose.Name = "tabClose";
            this.tabClose.SelectedTabPage = this.tapExpense;
            this.tabClose.Size = new System.Drawing.Size(837, 897);
            this.tabClose.TabIndex = 8;
            this.tabClose.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapExpense,
            this.tapMajor});
            // 
            // tapExpense
            // 
            this.tapExpense.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabClose.SetLanguageKey(this.tapExpense, "OUTSOURCINGCOSTEXPENSE");
            this.tapExpense.Name = "tapExpense";
            this.tapExpense.Size = new System.Drawing.Size(830, 861);
            this.tapExpense.Text = "xtraTabPage1";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdCostClosing, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(830, 861);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdCostClosing
            // 
            this.grdCostClosing.Caption = "";
            this.grdCostClosing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCostClosing.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCostClosing.IsUsePaging = false;
            this.grdCostClosing.LanguageKey = "OUTSOURCINGCOSTCLOSINGLIST";
            this.grdCostClosing.Location = new System.Drawing.Point(0, 40);
            this.grdCostClosing.Margin = new System.Windows.Forms.Padding(0);
            this.grdCostClosing.Name = "grdCostClosing";
            this.grdCostClosing.ShowBorder = true;
            this.grdCostClosing.ShowStatusBar = false;
            this.grdCostClosing.Size = new System.Drawing.Size(830, 821);
            this.grdCostClosing.TabIndex = 6;
            this.grdCostClosing.UseAutoBestFitColumns = false;
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.txtperiod);
            this.smartPanel2.Controls.Add(this.lblPeriod);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(830, 40);
            this.smartPanel2.TabIndex = 0;
            // 
            // txtperiod
            // 
            this.txtperiod.LabelText = null;
            this.txtperiod.LanguageKey = null;
            this.txtperiod.Location = new System.Drawing.Point(118, 7);
            this.txtperiod.Name = "txtperiod";
            this.txtperiod.Properties.ReadOnly = true;
            this.txtperiod.Size = new System.Drawing.Size(226, 24);
            this.txtperiod.TabIndex = 6;
            this.txtperiod.EditValueChanged += new System.EventHandler(this.txtperiod_EditValueChanged);
            // 
            // lblPeriod
            // 
            this.lblPeriod.LanguageKey = "PERIODDESCRIPTION";
            this.lblPeriod.Location = new System.Drawing.Point(17, 10);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(79, 18);
            this.lblPeriod.TabIndex = 5;
            this.lblPeriod.Text = "smartLabel1";
            // 
            // tapMajor
            // 
            this.tapMajor.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.tabClose.SetLanguageKey(this.tapMajor, "OUTSOURCINGCOSTMAJOR");
            this.tapMajor.Name = "tapMajor";
            this.tapMajor.Size = new System.Drawing.Size(830, 861);
            this.tapMajor.Text = "xtraTabPage2";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdCostClosingMajor, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartPanel3, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(830, 861);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // grdCostClosingMajor
            // 
            this.grdCostClosingMajor.Caption = "";
            this.grdCostClosingMajor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCostClosingMajor.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCostClosingMajor.IsUsePaging = false;
            this.grdCostClosingMajor.LanguageKey = "OUTSOURCINGCOSTCLOSINGMAJORLIST";
            this.grdCostClosingMajor.Location = new System.Drawing.Point(0, 40);
            this.grdCostClosingMajor.Margin = new System.Windows.Forms.Padding(0);
            this.grdCostClosingMajor.Name = "grdCostClosingMajor";
            this.grdCostClosingMajor.ShowBorder = true;
            this.grdCostClosingMajor.ShowStatusBar = false;
            this.grdCostClosingMajor.Size = new System.Drawing.Size(830, 821);
            this.grdCostClosingMajor.TabIndex = 6;
            this.grdCostClosingMajor.UseAutoBestFitColumns = false;
            // 
            // smartPanel3
            // 
            this.smartPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel3.Controls.Add(this.btnSettlemantProcess);
            this.smartPanel3.Controls.Add(this.btnSettlemantList);
            this.smartPanel3.Controls.Add(this.txtperiodMajor);
            this.smartPanel3.Controls.Add(this.smartLabel1);
            this.smartPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel3.Location = new System.Drawing.Point(0, 0);
            this.smartPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel3.Name = "smartPanel3";
            this.smartPanel3.Size = new System.Drawing.Size(830, 40);
            this.smartPanel3.TabIndex = 0;
            // 
            // btnSettlemantProcess
            // 
            this.btnSettlemantProcess.AllowFocus = false;
            this.btnSettlemantProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettlemantProcess.IsBusy = false;
            this.btnSettlemantProcess.IsWrite = true;
            this.btnSettlemantProcess.LanguageKey = "SETTLEMENTPROCESS";
            this.btnSettlemantProcess.Location = new System.Drawing.Point(647, 6);
            this.btnSettlemantProcess.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSettlemantProcess.Name = "btnSettlemantProcess";
            this.btnSettlemantProcess.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSettlemantProcess.Size = new System.Drawing.Size(80, 25);
            this.btnSettlemantProcess.TabIndex = 7;
            this.btnSettlemantProcess.Text = "정산처리";
            this.btnSettlemantProcess.TooltipLanguageKey = "";
            this.btnSettlemantProcess.Visible = false;
            // 
            // btnSettlemantList
            // 
            this.btnSettlemantList.AllowFocus = false;
            this.btnSettlemantList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettlemantList.IsBusy = false;
            this.btnSettlemantList.IsWrite = true;
            this.btnSettlemantList.LanguageKey = "SETTLEMENTLIST";
            this.btnSettlemantList.Location = new System.Drawing.Point(739, 6);
            this.btnSettlemantList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSettlemantList.Name = "btnSettlemantList";
            this.btnSettlemantList.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSettlemantList.Size = new System.Drawing.Size(80, 25);
            this.btnSettlemantList.TabIndex = 8;
            this.btnSettlemantList.Text = "정산내역";
            this.btnSettlemantList.TooltipLanguageKey = "";
            this.btnSettlemantList.Visible = false;
            // 
            // txtperiodMajor
            // 
            this.txtperiodMajor.LabelText = null;
            this.txtperiodMajor.LanguageKey = null;
            this.txtperiodMajor.Location = new System.Drawing.Point(118, 7);
            this.txtperiodMajor.Name = "txtperiodMajor";
            this.txtperiodMajor.Properties.ReadOnly = true;
            this.txtperiodMajor.Size = new System.Drawing.Size(226, 24);
            this.txtperiodMajor.TabIndex = 6;
            // 
            // smartLabel1
            // 
            this.smartLabel1.LanguageKey = "PERIODDESCRIPTION";
            this.smartLabel1.Location = new System.Drawing.Point(17, 10);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(79, 18);
            this.smartLabel1.TabIndex = 5;
            this.smartLabel1.Text = "smartLabel1";
            // 
            // btnCloseSend
            // 
            this.btnCloseSend.AllowFocus = false;
            this.btnCloseSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseSend.IsBusy = false;
            this.btnCloseSend.IsWrite = true;
            this.btnCloseSend.LanguageKey = "OUTSOURCECLOSESEND";
            this.btnCloseSend.Location = new System.Drawing.Point(1258, 4);
            this.btnCloseSend.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCloseSend.Name = "btnCloseSend";
            this.btnCloseSend.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCloseSend.Size = new System.Drawing.Size(80, 25);
            this.btnCloseSend.TabIndex = 3;
            this.btnCloseSend.Text = "마감전송";
            this.btnCloseSend.TooltipLanguageKey = "";
            this.btnCloseSend.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = true;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(1086, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // btnAggregate
            // 
            this.btnAggregate.AllowFocus = false;
            this.btnAggregate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAggregate.IsBusy = false;
            this.btnAggregate.IsWrite = true;
            this.btnAggregate.LanguageKey = "OUTSOURCEAGGREGATE";
            this.btnAggregate.Location = new System.Drawing.Point(1172, 5);
            this.btnAggregate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnAggregate.Name = "btnAggregate";
            this.btnAggregate.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAggregate.Size = new System.Drawing.Size(80, 25);
            this.btnAggregate.TabIndex = 4;
            this.btnAggregate.Text = "외주비집계";
            this.btnAggregate.TooltipLanguageKey = "";
            this.btnAggregate.Visible = false;
            // 
            // OutsourcingCostClosing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcingCostClosing";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabClose)).EndInit();
            this.tabClose.ResumeLayout(false);
            this.tapExpense.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.smartPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiod.Properties)).EndInit();
            this.tapMajor.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).EndInit();
            this.smartPanel3.ResumeLayout(false);
            this.smartPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiodMajor.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdCostClosing;
        private Framework.SmartControls.SmartButton btnCloseSend;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnAggregate;
        private Framework.SmartControls.SmartTextBox txtperiod;
        private Framework.SmartControls.SmartLabel lblPeriod;
        private Framework.SmartControls.SmartTabControl tabClose;
        private DevExpress.XtraTab.XtraTabPage tapExpense;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private DevExpress.XtraTab.XtraTabPage tapMajor;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdCostClosingMajor;
        private Framework.SmartControls.SmartPanel smartPanel3;
        private Framework.SmartControls.SmartTextBox txtperiodMajor;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartButton btnSettlemantProcess;
        private Framework.SmartControls.SmartButton btnSettlemantList;
    }
}