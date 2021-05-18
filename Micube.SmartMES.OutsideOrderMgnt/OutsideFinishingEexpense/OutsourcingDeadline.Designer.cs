namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingDeadline
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
            this.grdPeriod = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabClose = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapExpense = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tapMajor = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdPeriodMajor = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabClose)).BeginInit();
            this.tabClose.SuspendLayout();
            this.tapExpense.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.tapMajor.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabClose);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // grdPeriod
            // 
            this.grdPeriod.Caption = "";
            this.grdPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPeriod.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPeriod.IsUsePaging = false;
            this.grdPeriod.LanguageKey = "OUTSOURCINGDEADLINELIST";
            this.grdPeriod.Location = new System.Drawing.Point(0, 0);
            this.grdPeriod.Margin = new System.Windows.Forms.Padding(0);
            this.grdPeriod.Name = "grdPeriod";
            this.grdPeriod.ShowBorder = true;
            this.grdPeriod.ShowStatusBar = false;
            this.grdPeriod.Size = new System.Drawing.Size(838, 875);
            this.grdPeriod.TabIndex = 5;
            this.grdPeriod.UseAutoBestFitColumns = false;
            this.grdPeriod.Load += new System.EventHandler(this.grdPeriod_Load);
            // 
            // tabClose
            // 
            this.tabClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabClose.Location = new System.Drawing.Point(0, 0);
            this.tabClose.Name = "tabClose";
            this.tabClose.SelectedTabPage = this.tapExpense;
            this.tabClose.Size = new System.Drawing.Size(845, 911);
            this.tabClose.TabIndex = 9;
            this.tabClose.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapExpense,
            this.tapMajor});
            // 
            // tapExpense
            // 
            this.tapExpense.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabClose.SetLanguageKey(this.tapExpense, "OUTSOURCINGCOSTEXPENSE");
            this.tapExpense.Name = "tapExpense";
            this.tapExpense.Size = new System.Drawing.Size(838, 875);
            this.tapExpense.Text = "xtraTabPage1";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdPeriod, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 436F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(838, 875);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // tapMajor
            // 
            this.tapMajor.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.tabClose.SetLanguageKey(this.tapMajor, "OUTSOURCINGCOSTMAJOR");
            this.tapMajor.Name = "tapMajor";
            this.tapMajor.Size = new System.Drawing.Size(663, 436);
            this.tapMajor.Text = "xtraTabPage2";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdPeriodMajor, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 875F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(663, 436);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // grdPeriodMajor
            // 
            this.grdPeriodMajor.Caption = "";
            this.grdPeriodMajor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPeriodMajor.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPeriodMajor.IsUsePaging = false;
            this.grdPeriodMajor.LanguageKey = "OUTSOURCINGCOSTCLOSINGMAJORLIST";
            this.grdPeriodMajor.Location = new System.Drawing.Point(0, 0);
            this.grdPeriodMajor.Margin = new System.Windows.Forms.Padding(0);
            this.grdPeriodMajor.Name = "grdPeriodMajor";
            this.grdPeriodMajor.ShowBorder = true;
            this.grdPeriodMajor.ShowStatusBar = false;
            this.grdPeriodMajor.Size = new System.Drawing.Size(663, 436);
            this.grdPeriodMajor.TabIndex = 6;
            this.grdPeriodMajor.UseAutoBestFitColumns = false;
            // 
            // OutsourcingDeadline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "OutsourcingDeadline";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabClose)).EndInit();
            this.tabClose.ResumeLayout(false);
            this.tapExpense.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.tapMajor.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdPeriod;
        private Framework.SmartControls.SmartTabControl tabClose;
        private DevExpress.XtraTab.XtraTabPage tapExpense;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private DevExpress.XtraTab.XtraTabPage tapMajor;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdPeriodMajor;
    }
}