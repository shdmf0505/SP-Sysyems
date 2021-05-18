namespace Micube.SmartMES.QualityAnalysis
{
    partial class AuditManageRegistQuarterPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditManageRegistQuarterPopup));
            this.tlpInspection = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblRemark = new Micube.Framework.SmartControls.SmartLabel();
            this.txtRemark = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.grdAuditManageregist = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.cboInspectionPlanWeek = new Micube.Framework.SmartControls.SmartComboBox();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.fpcReport = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.cboInspectionPlanMonth = new Micube.Framework.SmartControls.SmartComboBox();
            this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tlpInspection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboInspectionPlanWeek.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboInspectionPlanMonth.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlMain.Size = new System.Drawing.Size(1143, 721);
            // 
            // tlpInspection
            // 
            this.tlpInspection.ColumnCount = 2;
            this.tlpInspection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpInspection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInspection.Controls.Add(this.lblRemark, 0, 1);
            this.tlpInspection.Controls.Add(this.txtRemark, 1, 1);
            this.tlpInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInspection.Location = new System.Drawing.Point(0, 324);
            this.tlpInspection.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpInspection.Name = "tlpInspection";
            this.tlpInspection.RowCount = 1;
            this.tlpInspection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlpInspection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInspection.Size = new System.Drawing.Size(1143, 85);
            this.tlpInspection.TabIndex = 2;
            // 
            // lblRemark
            // 
            this.lblRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemark.LanguageKey = "DESCRIPTION";
            this.lblRemark.Location = new System.Drawing.Point(3, 1);
            this.lblRemark.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 79);
            this.lblRemark.TabIndex = 3;
            this.lblRemark.Text = "비고:";
            // 
            // txtRemark
            // 
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(58, 4);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(1082, 78);
            this.txtRemark.TabIndex = 4;
            // 
            // grdAuditManageregist
            // 
            this.grdAuditManageregist.Caption = "정기등록정보";
            this.grdAuditManageregist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAuditManageregist.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAuditManageregist.IsUsePaging = false;
            this.grdAuditManageregist.LanguageKey = null;
            this.grdAuditManageregist.Location = new System.Drawing.Point(0, 39);
            this.grdAuditManageregist.Margin = new System.Windows.Forms.Padding(0);
            this.grdAuditManageregist.Name = "grdAuditManageregist";
            this.grdAuditManageregist.ShowBorder = false;
            this.grdAuditManageregist.ShowStatusBar = false;
            this.grdAuditManageregist.Size = new System.Drawing.Size(1143, 285);
            this.grdAuditManageregist.TabIndex = 0;
            this.grdAuditManageregist.UseAutoBestFitColumns = false;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.LanguageKey = "AUDITPLANDATE";
            this.smartLabel1.Location = new System.Drawing.Point(3, 0);
            this.smartLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(91, 34);
            this.smartLabel1.TabIndex = 3;
            this.smartLabel1.Text = "정기심사계획일";
            // 
            // cboInspectionPlanWeek
            // 
            this.cboInspectionPlanWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboInspectionPlanWeek.LabelText = null;
            this.cboInspectionPlanWeek.LanguageKey = null;
            this.cboInspectionPlanWeek.Location = new System.Drawing.Point(217, 8);
            this.cboInspectionPlanWeek.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.cboInspectionPlanWeek.Name = "cboInspectionPlanWeek";
            this.cboInspectionPlanWeek.PopupWidth = 0;
            this.cboInspectionPlanWeek.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboInspectionPlanWeek.Properties.NullText = "";
            this.cboInspectionPlanWeek.ShowHeader = true;
            this.cboInspectionPlanWeek.Size = new System.Drawing.Size(72, 20);
            this.cboInspectionPlanWeek.TabIndex = 5;
            this.cboInspectionPlanWeek.VisibleColumns = null;
            this.cboInspectionPlanWeek.VisibleColumnsWidth = null;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdAuditManageregist, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tlpInspection, 0, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.fpcReport, 0, 3);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSplitTableLayoutPanel3, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 4;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 285F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 268F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(1143, 721);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // fpcReport
            // 
            this.fpcReport.countRows = false;
            this.fpcReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpcReport.executeFileAfterDown = false;
            this.fpcReport.LanguageKey = "";
            this.fpcReport.Location = new System.Drawing.Point(0, 409);
            this.fpcReport.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.fpcReport.Name = "fpcReport";
            this.fpcReport.showImage = false;
            this.fpcReport.Size = new System.Drawing.Size(1143, 298);
            this.fpcReport.TabIndex = 6;
            this.fpcReport.UploadPath = "";
            this.fpcReport.UseCommentsColumn = true;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 8;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 359F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 263F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.cboInspectionPlanMonth, 0, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartLabel3, 4, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.cboInspectionPlanWeek, 3, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartLabel2, 2, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartLabel1, 0, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 7, 0);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 1;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(1143, 39);
            this.smartSplitTableLayoutPanel3.TabIndex = 8;
            // 
            // cboInspectionPlanMonth
            // 
            this.cboInspectionPlanMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboInspectionPlanMonth.LabelText = null;
            this.cboInspectionPlanMonth.LanguageKey = null;
            this.cboInspectionPlanMonth.Location = new System.Drawing.Point(97, 8);
            this.cboInspectionPlanMonth.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.cboInspectionPlanMonth.Name = "cboInspectionPlanMonth";
            this.cboInspectionPlanMonth.PopupWidth = 0;
            this.cboInspectionPlanMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboInspectionPlanMonth.Properties.NullText = "";
            this.cboInspectionPlanMonth.ShowHeader = true;
            this.cboInspectionPlanMonth.Size = new System.Drawing.Size(84, 20);
            this.cboInspectionPlanMonth.TabIndex = 8;
            this.cboInspectionPlanMonth.VisibleColumns = null;
            this.cboInspectionPlanMonth.VisibleColumnsWidth = null;
            // 
            // smartLabel3
            // 
            this.smartLabel3.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.smartLabel3.Appearance.Options.UseFont = true;
            this.smartLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel3.Location = new System.Drawing.Point(295, 3);
            this.smartLabel3.Name = "smartLabel3";
            this.smartLabel3.Size = new System.Drawing.Size(353, 33);
            this.smartLabel3.TabIndex = 7;
            this.smartLabel3.Text = "주";
            // 
            // smartLabel2
            // 
            this.smartLabel2.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.smartLabel2.Appearance.Options.UseFont = true;
            this.smartLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel2.Location = new System.Drawing.Point(187, 3);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(24, 33);
            this.smartLabel2.TabIndex = 6;
            this.smartLabel2.Text = "월";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btnClose);
            this.flowLayoutPanel2.Controls.Add(this.btnSave);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(968, 10);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(169, 25);
            this.flowLayoutPanel2.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(89, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(3, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // AuditManageRegistQuarterPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 741);
            this.LanguageKey = "AUDITMANAGEREGISTQUARTERPOPUP";
            this.Name = "AuditManageRegistQuarterPopup";
            this.Text = "정기 등록/수정";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tlpInspection.ResumeLayout(false);
            this.tlpInspection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboInspectionPlanWeek.Properties)).EndInit();
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboInspectionPlanMonth.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpInspection;
        private Framework.SmartControls.SmartLabel lblRemark;
        private Framework.SmartControls.SmartBandedGrid grdAuditManageregist;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartComboBox cboInspectionPlanWeek;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Commons.Controls.SmartFileProcessingControl fpcReport;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        private Framework.SmartControls.SmartLabel smartLabel3;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartMemoEdit txtRemark;
        private Framework.SmartControls.SmartComboBox cboInspectionPlanMonth;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnClose;
        public Framework.SmartControls.SmartButton btnSave;
    }
}