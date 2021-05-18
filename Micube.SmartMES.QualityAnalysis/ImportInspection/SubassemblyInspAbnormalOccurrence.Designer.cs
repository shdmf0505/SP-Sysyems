namespace Micube.SmartMES.QualityAnalysis
{
    partial class SubassemblyInspAbnormalOccurrence
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
            this.spcAbnormal = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpInspectionResultRegister = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemRawAssy = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemSpecRawAssy = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdRawAssyAbnormal = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.spcAbnormal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInspectionResultRegister)).BeginInit();
            this.grpInspectionResultRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.spcAbnormal);
            // 
            // spcAbnormal
            // 
            this.spcAbnormal.ColumnCount = 1;
            this.spcAbnormal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.spcAbnormal.Controls.Add(this.grpInspectionResultRegister, 0, 2);
            this.spcAbnormal.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.spcAbnormal.Controls.Add(this.grdRawAssyAbnormal, 0, 0);
            this.spcAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcAbnormal.Location = new System.Drawing.Point(0, 0);
            this.spcAbnormal.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcAbnormal.Name = "spcAbnormal";
            this.spcAbnormal.RowCount = 3;
            this.spcAbnormal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcAbnormal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.spcAbnormal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcAbnormal.Size = new System.Drawing.Size(475, 401);
            this.spcAbnormal.TabIndex = 0;
            // 
            // grpInspectionResultRegister
            // 
            this.grpInspectionResultRegister.Controls.Add(this.smartTabControl1);
            this.grpInspectionResultRegister.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspectionResultRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspectionResultRegister.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpInspectionResultRegister.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspectionResultRegister.LanguageKey = "INSPECTIONRESULT";
            this.grpInspectionResultRegister.Location = new System.Drawing.Point(0, 205);
            this.grpInspectionResultRegister.Margin = new System.Windows.Forms.Padding(0);
            this.grpInspectionResultRegister.Name = "grpInspectionResultRegister";
            this.grpInspectionResultRegister.ShowBorder = true;
            this.grpInspectionResultRegister.Size = new System.Drawing.Size(475, 196);
            this.grpInspectionResultRegister.TabIndex = 9;
            this.grpInspectionResultRegister.Text = "INSPECTIONRESULTREGISTER";
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTabControl1.Location = new System.Drawing.Point(2, 31);
            this.smartTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.smartTabControl1.Size = new System.Drawing.Size(471, 163);
            this.smartTabControl1.TabIndex = 7;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdInspectionItemRawAssy);
            this.smartTabControl1.SetLanguageKey(this.xtraTabPage1, "EXTERIORINSPECTION");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Padding = new System.Windows.Forms.Padding(10);
            this.xtraTabPage1.Size = new System.Drawing.Size(465, 134);
            this.xtraTabPage1.Text = "DEFECTINSPECTION";
            // 
            // grdInspectionItemRawAssy
            // 
            this.grdInspectionItemRawAssy.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionItemRawAssy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionItemRawAssy.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionItemRawAssy.IsUsePaging = false;
            this.grdInspectionItemRawAssy.LanguageKey = null;
            this.grdInspectionItemRawAssy.Location = new System.Drawing.Point(10, 10);
            this.grdInspectionItemRawAssy.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionItemRawAssy.Name = "grdInspectionItemRawAssy";
            this.grdInspectionItemRawAssy.ShowBorder = false;
            this.grdInspectionItemRawAssy.ShowButtonBar = false;
            this.grdInspectionItemRawAssy.ShowStatusBar = false;
            this.grdInspectionItemRawAssy.Size = new System.Drawing.Size(445, 114);
            this.grdInspectionItemRawAssy.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdInspectionItemSpecRawAssy);
            this.smartTabControl1.SetLanguageKey(this.xtraTabPage2, "MEASUREINSPECTION");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(10);
            this.xtraTabPage2.Size = new System.Drawing.Size(465, 134);
            this.xtraTabPage2.Text = "MEASUREINSPECTION";
            // 
            // grdInspectionItemSpecRawAssy
            // 
            this.grdInspectionItemSpecRawAssy.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionItemSpecRawAssy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionItemSpecRawAssy.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionItemSpecRawAssy.IsUsePaging = false;
            this.grdInspectionItemSpecRawAssy.LanguageKey = null;
            this.grdInspectionItemSpecRawAssy.Location = new System.Drawing.Point(10, 10);
            this.grdInspectionItemSpecRawAssy.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionItemSpecRawAssy.Name = "grdInspectionItemSpecRawAssy";
            this.grdInspectionItemSpecRawAssy.ShowBorder = false;
            this.grdInspectionItemSpecRawAssy.ShowButtonBar = false;
            this.grdInspectionItemSpecRawAssy.ShowStatusBar = false;
            this.grdInspectionItemSpecRawAssy.Size = new System.Drawing.Size(445, 114);
            this.grdInspectionItemSpecRawAssy.TabIndex = 0;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 195);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(475, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdRawAssyAbnormal
            // 
            this.grdRawAssyAbnormal.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRawAssyAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawAssyAbnormal.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRawAssyAbnormal.IsUsePaging = false;
            this.grdRawAssyAbnormal.LanguageKey = "SUBASSEMBLYABNORMALOCCURRENCE";
            this.grdRawAssyAbnormal.Location = new System.Drawing.Point(0, 0);
            this.grdRawAssyAbnormal.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawAssyAbnormal.Name = "grdRawAssyAbnormal";
            this.grdRawAssyAbnormal.ShowBorder = true;
            this.grdRawAssyAbnormal.Size = new System.Drawing.Size(475, 195);
            this.grdRawAssyAbnormal.TabIndex = 1;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(395, -2);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 6;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // SubassemblyInspAbnormalOccurrence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SubassemblyInspAbnormalOccurrence";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.spcAbnormal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInspectionResultRegister)).EndInit();
            this.grpInspectionResultRegister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel spcAbnormal;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdRawAssyAbnormal;
        private Framework.SmartControls.SmartGroupBox grpInspectionResultRegister;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemRawAssy;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemSpecRawAssy;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}