namespace Micube.SmartMES.QualityAnalysis
{
    partial class AuditManageRegistPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditManageRegistPopup));
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartLayoutControl2 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.fpcReport = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.txtRemark = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.smartLayoutControlGroup2 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.grdAuditManageregist = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).BeginInit();
            this.smartLayoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartLayoutControl1);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Size = new System.Drawing.Size(1178, 755);
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.Location = new System.Drawing.Point(1078, 731);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(98, 22);
            this.btnClose.StyleController = this.smartLayoutControl1;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartLayoutControl1.Controls.Add(this.btnSave);
            this.smartLayoutControl1.Controls.Add(this.btnClose);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1200, 234, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(1178, 755);
            this.smartLayoutControl1.TabIndex = 2;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartGroupBox1, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdAuditManageregist, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1174, 725);
            this.smartSplitTableLayoutPanel1.TabIndex = 3;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.smartLayoutControl2);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 296);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(1174, 429);
            this.smartGroupBox1.TabIndex = 2;
            // 
            // smartLayoutControl2
            // 
            this.smartLayoutControl2.Controls.Add(this.fpcReport);
            this.smartLayoutControl2.Controls.Add(this.txtRemark);
            this.smartLayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl2.Location = new System.Drawing.Point(2, 31);
            this.smartLayoutControl2.Name = "smartLayoutControl2";
            this.smartLayoutControl2.Root = this.smartLayoutControlGroup2;
            this.smartLayoutControl2.Size = new System.Drawing.Size(1170, 396);
            this.smartLayoutControl2.TabIndex = 0;
            this.smartLayoutControl2.Text = "smartLayoutControl2";
            // 
            // fpcReport
            // 
            this.fpcReport.countRows = false;
            this.fpcReport.executeFileAfterDown = false;
            this.fpcReport.LanguageKey = "";
            this.fpcReport.Location = new System.Drawing.Point(5, 87);
            this.fpcReport.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.fpcReport.Name = "fpcReport";
            this.fpcReport.showImage = false;
            this.fpcReport.Size = new System.Drawing.Size(1160, 304);
            this.fpcReport.TabIndex = 8;
            this.fpcReport.UploadPath = "";
            this.fpcReport.UseCommentsColumn = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(113, 5);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(1052, 78);
            this.txtRemark.StyleController = this.smartLayoutControl2;
            this.txtRemark.TabIndex = 5;
            // 
            // smartLayoutControlGroup2
            // 
            this.smartLayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup2.GroupBordersVisible = false;
            this.smartLayoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.smartLayoutControlGroup2.Name = "smartLayoutControlGroup2";
            this.smartLayoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.smartLayoutControlGroup2.Size = new System.Drawing.Size(1170, 396);
            this.smartLayoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtRemark;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1164, 82);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.fpcReport;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 82);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1164, 308);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // grdAuditManageregist
            // 
            this.grdAuditManageregist.Caption = "";
            this.grdAuditManageregist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAuditManageregist.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAuditManageregist.IsUsePaging = false;
            this.grdAuditManageregist.LanguageKey = null;
            this.grdAuditManageregist.Location = new System.Drawing.Point(0, 0);
            this.grdAuditManageregist.Margin = new System.Windows.Forms.Padding(0);
            this.grdAuditManageregist.Name = "grdAuditManageregist";
            this.grdAuditManageregist.ShowBorder = true;
            this.grdAuditManageregist.Size = new System.Drawing.Size(1174, 286);
            this.grdAuditManageregist.TabIndex = 1;
            this.grdAuditManageregist.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 286);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(1174, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.Location = new System.Drawing.Point(976, 731);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(98, 22);
            this.btnSave.StyleController = this.smartLayoutControl1;
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(1178, 755);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnClose;
            this.layoutControlItem4.Location = new System.Drawing.Point(1076, 729);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(102, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(102, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(102, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            this.layoutControlItem5.Location = new System.Drawing.Point(974, 729);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(102, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(102, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(102, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 729);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(974, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.smartSplitTableLayoutPanel1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1178, 729);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // AuditManageRegistPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Name = "AuditManageRegistPopup";
            this.Padding = new System.Windows.Forms.Padding(3);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).EndInit();
            this.smartLayoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartButton btnClose;
        public Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl2;
        private Commons.Controls.SmartFileProcessingControl fpcReport;
        private Framework.SmartControls.SmartMemoEdit txtRemark;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Framework.SmartControls.SmartBandedGrid grdAuditManageregist;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}