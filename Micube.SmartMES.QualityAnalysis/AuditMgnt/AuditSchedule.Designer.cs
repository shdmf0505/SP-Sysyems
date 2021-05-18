namespace Micube.SmartMES.QualityAnalysis
{
    partial class AuditSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditSchedule));
            this.tabAuditSchedule = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgAuditSchedule = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdSubsidiary = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdSemiProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgSheetForm = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdSheetForm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.fileSheetForm = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAuditSchedule)).BeginInit();
            this.tabAuditSchedule.SuspendLayout();
            this.tpgAuditSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.tpgSheetForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 783);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(746, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabAuditSchedule);
            this.pnlContent.Size = new System.Drawing.Size(746, 787);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1051, 816);
            // 
            // tabAuditSchedule
            // 
            this.tabAuditSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAuditSchedule.Location = new System.Drawing.Point(0, 0);
            this.tabAuditSchedule.Name = "tabAuditSchedule";
            this.tabAuditSchedule.SelectedTabPage = this.tpgAuditSchedule;
            this.tabAuditSchedule.Size = new System.Drawing.Size(746, 787);
            this.tabAuditSchedule.TabIndex = 0;
            this.tabAuditSchedule.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgAuditSchedule,
            this.tpgSheetForm});
            // 
            // tpgAuditSchedule
            // 
            this.tpgAuditSchedule.Controls.Add(this.smartSpliterContainer1);
            this.tabAuditSchedule.SetLanguageKey(this.tpgAuditSchedule, "AUDITSCHEDULE");
            this.tpgAuditSchedule.Name = "tpgAuditSchedule";
            this.tpgAuditSchedule.Size = new System.Drawing.Size(740, 758);
            this.tpgAuditSchedule.Text = "Audit 계획";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdSubsidiary);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdSemiProduct);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(740, 758);
            this.smartSpliterContainer1.SplitterPosition = 400;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdSubsidiary
            // 
            this.grdSubsidiary.Caption = "부자재 부문 [6]개 공정";
            this.grdSubsidiary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSubsidiary.IsUsePaging = false;
            this.grdSubsidiary.LanguageKey = null;
            this.grdSubsidiary.Location = new System.Drawing.Point(0, 0);
            this.grdSubsidiary.Margin = new System.Windows.Forms.Padding(0);
            this.grdSubsidiary.Name = "grdSubsidiary";
            this.grdSubsidiary.ShowBorder = true;
            this.grdSubsidiary.ShowStatusBar = false;
            this.grdSubsidiary.Size = new System.Drawing.Size(740, 400);
            this.grdSubsidiary.TabIndex = 0;
            // 
            // grdSemiProduct
            // 
            this.grdSemiProduct.Caption = "반제품 부문 [24]개 공정";
            this.grdSemiProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSemiProduct.IsUsePaging = false;
            this.grdSemiProduct.LanguageKey = null;
            this.grdSemiProduct.Location = new System.Drawing.Point(0, 0);
            this.grdSemiProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdSemiProduct.Name = "grdSemiProduct";
            this.grdSemiProduct.ShowBorder = true;
            this.grdSemiProduct.ShowStatusBar = false;
            this.grdSemiProduct.Size = new System.Drawing.Size(740, 353);
            this.grdSemiProduct.TabIndex = 0;
            // 
            // tpgSheetForm
            // 
            this.tpgSheetForm.Controls.Add(this.smartSpliterContainer2);
            this.tabAuditSchedule.SetLanguageKey(this.tpgSheetForm, "SHEETFORM");
            this.tpgSheetForm.Name = "tpgSheetForm";
            this.tpgSheetForm.Size = new System.Drawing.Size(740, 758);
            this.tpgSheetForm.Text = "점검 시트 양식";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdSheetForm);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.fileSheetForm);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(740, 758);
            this.smartSpliterContainer2.SplitterPosition = 400;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdSheetForm
            // 
            this.grdSheetForm.Caption = "점검 시트 양식";
            this.grdSheetForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSheetForm.IsUsePaging = false;
            this.grdSheetForm.LanguageKey = null;
            this.grdSheetForm.Location = new System.Drawing.Point(0, 0);
            this.grdSheetForm.Margin = new System.Windows.Forms.Padding(0);
            this.grdSheetForm.Name = "grdSheetForm";
            this.grdSheetForm.ShowBorder = true;
            this.grdSheetForm.ShowStatusBar = false;
            this.grdSheetForm.Size = new System.Drawing.Size(740, 400);
            this.grdSheetForm.TabIndex = 0;
            // 
            // fileSheetForm
            // 
            this.fileSheetForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileSheetForm.LanguageKey = "";
            this.fileSheetForm.Location = new System.Drawing.Point(0, 0);
            this.fileSheetForm.Margin = new System.Windows.Forms.Padding(0);
            this.fileSheetForm.Name = "fileSheetForm";
            this.fileSheetForm.Size = new System.Drawing.Size(740, 353);
            this.fileSheetForm.TabIndex = 0;
            this.fileSheetForm.UploadPath = "";
            this.fileSheetForm.UseCommentsColumn = true;
            // 
            // AuditSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 836);
            this.Name = "AuditSchedule";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAuditSchedule)).EndInit();
            this.tabAuditSchedule.ResumeLayout(false);
            this.tpgAuditSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.tpgSheetForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabAuditSchedule;
        private DevExpress.XtraTab.XtraTabPage tpgAuditSchedule;
        private DevExpress.XtraTab.XtraTabPage tpgSheetForm;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdSubsidiary;
        private Framework.SmartControls.SmartBandedGrid grdSemiProduct;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdSheetForm;
        private Commons.Controls.SmartFileProcessingControl fileSheetForm;
    }
}