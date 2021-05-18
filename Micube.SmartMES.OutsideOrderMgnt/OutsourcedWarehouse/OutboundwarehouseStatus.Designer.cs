namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutboundwarehouseStatus
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
            this.tabInoutInquiry = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapImportInspect = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdImportInspect = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapInOutBound = new DevExpress.XtraTab.XtraTabPage();
            this.grdInOUtBound = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapOutputslip = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdOutputslip = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnOutputslip = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInoutInquiry)).BeginInit();
            this.tabInoutInquiry.SuspendLayout();
            this.tapImportInspect.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.tapInOutBound.SuspendLayout();
            this.tapOutputslip.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnOutputslip);
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnOutputslip, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabInoutInquiry);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // tabInoutInquiry
            // 
            this.tabInoutInquiry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInoutInquiry.Location = new System.Drawing.Point(0, 0);
            this.tabInoutInquiry.Margin = new System.Windows.Forms.Padding(0);
            this.tabInoutInquiry.Name = "tabInoutInquiry";
            this.tabInoutInquiry.SelectedTabPage = this.tapImportInspect;
            this.tabInoutInquiry.Size = new System.Drawing.Size(845, 911);
            this.tabInoutInquiry.TabIndex = 1;
            this.tabInoutInquiry.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapImportInspect,
            this.tapInOutBound,
            this.tapOutputslip});
            // 
            // tapImportInspect
            // 
            this.tapImportInspect.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.tabInoutInquiry.SetLanguageKey(this.tapImportInspect, "RequestImportInspection");
            this.tapImportInspect.Name = "tapImportInspect";
            this.tapImportInspect.Size = new System.Drawing.Size(838, 875);
            this.tapImportInspect.Text = "수입검사의뢰";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdImportInspect, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 592F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(838, 875);
            this.smartSplitTableLayoutPanel2.TabIndex = 0;
            // 
            // grdImportInspect
            // 
            this.grdImportInspect.Caption = "외주 창고 입출고 L/T 내역";
            this.grdImportInspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdImportInspect.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdImportInspect.IsUsePaging = false;
            this.grdImportInspect.LanguageKey = "REQUESTIMPORTINSPECTIONLIST";
            this.grdImportInspect.Location = new System.Drawing.Point(0, 0);
            this.grdImportInspect.Margin = new System.Windows.Forms.Padding(0);
            this.grdImportInspect.Name = "grdImportInspect";
            this.grdImportInspect.ShowBorder = true;
            this.grdImportInspect.ShowStatusBar = false;
            this.grdImportInspect.Size = new System.Drawing.Size(838, 875);
            this.grdImportInspect.TabIndex = 4;
            // 
            // tapInOutBound
            // 
            this.tapInOutBound.Controls.Add(this.grdInOUtBound);
            this.tabInoutInquiry.SetLanguageKey(this.tapInOutBound, "IOLTbreakdown");
            this.tapInOutBound.Name = "tapInOutBound";
            this.tapInOutBound.Size = new System.Drawing.Size(838, 875);
            this.tapInOutBound.Text = "입출고 L/T";
            // 
            // grdInOUtBound
            // 
            this.grdInOUtBound.Caption = "외주 창고 입출고 L/T 내역";
            this.grdInOUtBound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInOUtBound.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInOUtBound.IsUsePaging = false;
            this.grdInOUtBound.LanguageKey = "IOLTBREAKDOWNLIST";
            this.grdInOUtBound.Location = new System.Drawing.Point(0, 0);
            this.grdInOUtBound.Margin = new System.Windows.Forms.Padding(0);
            this.grdInOUtBound.Name = "grdInOUtBound";
            this.grdInOUtBound.ShowBorder = true;
            this.grdInOUtBound.ShowStatusBar = false;
            this.grdInOUtBound.Size = new System.Drawing.Size(838, 875);
            this.grdInOUtBound.TabIndex = 3;
            // 
            // tapOutputslip
            // 
            this.tapOutputslip.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabInoutInquiry.SetLanguageKey(this.tapOutputslip, "DocumentOutputHistory");
            this.tapOutputslip.Name = "tapOutputslip";
            this.tapOutputslip.Size = new System.Drawing.Size(838, 875);
            this.tapOutputslip.Text = "전표출력이력";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdOutputslip, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(838, 875);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdOutputslip
            // 
            this.grdOutputslip.Caption = "출고전표 출력 이력";
            this.grdOutputslip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOutputslip.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOutputslip.IsUsePaging = false;
            this.grdOutputslip.LanguageKey = "DOCUMENTOUTPUTHISTORY";
            this.grdOutputslip.Location = new System.Drawing.Point(0, 0);
            this.grdOutputslip.Margin = new System.Windows.Forms.Padding(0);
            this.grdOutputslip.Name = "grdOutputslip";
            this.grdOutputslip.ShowBorder = true;
            this.grdOutputslip.ShowStatusBar = false;
            this.grdOutputslip.Size = new System.Drawing.Size(838, 875);
            this.grdOutputslip.TabIndex = 4;
            // 
            // btnOutputslip
            // 
            this.btnOutputslip.AllowFocus = false;
            this.btnOutputslip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputslip.IsBusy = false;
            this.btnOutputslip.IsWrite = true;
            this.btnOutputslip.LanguageKey = "OUTPUTSLIP";
            this.btnOutputslip.Location = new System.Drawing.Point(733, 5);
            this.btnOutputslip.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOutputslip.Name = "btnOutputslip";
            this.btnOutputslip.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOutputslip.Size = new System.Drawing.Size(105, 23);
            this.btnOutputslip.TabIndex = 0;
            this.btnOutputslip.Text = "출고전표";
            this.btnOutputslip.TooltipLanguageKey = "";
            this.btnOutputslip.Visible = false;
            // 
            // OutboundwarehouseStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "OutboundwarehouseStatus";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInoutInquiry)).EndInit();
            this.tabInoutInquiry.ResumeLayout(false);
            this.tapImportInspect.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.tapInOutBound.ResumeLayout(false);
            this.tapOutputslip.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabInoutInquiry;
        private DevExpress.XtraTab.XtraTabPage tapImportInspect;
        private DevExpress.XtraTab.XtraTabPage tapInOutBound;
        private Framework.SmartControls.SmartBandedGrid grdInOUtBound;
        private DevExpress.XtraTab.XtraTabPage tapOutputslip;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdOutputslip;
        private Framework.SmartControls.SmartButton btnOutputslip;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdImportInspect;
    }
}