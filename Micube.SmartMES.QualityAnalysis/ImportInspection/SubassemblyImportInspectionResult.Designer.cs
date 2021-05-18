namespace Micube.SmartMES.QualityAnalysis
{
    partial class SubassemblyImportInspectionResult
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
            this.btnRegister = new Micube.Framework.SmartControls.SmartButton();
            this.spcTable = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpInspItem = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tabInspectionResult = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDefectInspection = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgMeasureInspection = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcRawAssy = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdInspectionResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.spcTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInspItem)).BeginInit();
            this.grpInspItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionResult)).BeginInit();
            this.tabInspectionResult.SuspendLayout();
            this.tpgDefectInspection.SuspendLayout();
            this.tpgMeasureInspection.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnRegister);
            this.pnlToolbar.Controls.SetChildIndex(this.btnRegister, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.spcTable);
            // 
            // btnRegister
            // 
            this.btnRegister.AllowFocus = false;
            this.btnRegister.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRegister.IsBusy = false;
            this.btnRegister.IsWrite = false;
            this.btnRegister.LanguageKey = "REGISTRATION";
            this.btnRegister.Location = new System.Drawing.Point(395, 0);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRegister.Size = new System.Drawing.Size(80, 24);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "smartButton1";
            this.btnRegister.TooltipLanguageKey = "";
            this.btnRegister.Visible = false;
            // 
            // spcTable
            // 
            this.spcTable.ColumnCount = 1;
            this.spcTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.spcTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.spcTable.Controls.Add(this.grpInspItem, 0, 2);
            this.spcTable.Controls.Add(this.spcRawAssy, 0, 1);
            this.spcTable.Controls.Add(this.grdInspectionResult, 0, 0);
            this.spcTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcTable.Location = new System.Drawing.Point(0, 0);
            this.spcTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcTable.Name = "spcTable";
            this.spcTable.RowCount = 3;
            this.spcTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.spcTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcTable.Size = new System.Drawing.Size(475, 401);
            this.spcTable.TabIndex = 0;
            // 
            // grpInspItem
            // 
            this.grpInspItem.Controls.Add(this.tabInspectionResult);
            this.grpInspItem.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpInspItem.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspItem.LanguageKey = "INSPECTIONRESULT";
            this.grpInspItem.Location = new System.Drawing.Point(0, 205);
            this.grpInspItem.Margin = new System.Windows.Forms.Padding(0);
            this.grpInspItem.Name = "grpInspItem";
            this.grpInspItem.ShowBorder = true;
            this.grpInspItem.Size = new System.Drawing.Size(475, 196);
            this.grpInspItem.TabIndex = 10;
            this.grpInspItem.Text = "smartGroupBox1";
            // 
            // tabInspectionResult
            // 
            this.tabInspectionResult.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabInspectionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInspectionResult.Location = new System.Drawing.Point(2, 31);
            this.tabInspectionResult.Margin = new System.Windows.Forms.Padding(0);
            this.tabInspectionResult.Name = "tabInspectionResult";
            this.tabInspectionResult.SelectedTabPage = this.tpgDefectInspection;
            this.tabInspectionResult.Size = new System.Drawing.Size(471, 163);
            this.tabInspectionResult.TabIndex = 8;
            this.tabInspectionResult.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgDefectInspection,
            this.tpgMeasureInspection});
            // 
            // tpgDefectInspection
            // 
            this.tpgDefectInspection.Controls.Add(this.grdInspectionItem);
            this.tabInspectionResult.SetLanguageKey(this.tpgDefectInspection, "EXTERIORINSPECTION");
            this.tpgDefectInspection.Name = "tpgDefectInspection";
            this.tpgDefectInspection.Padding = new System.Windows.Forms.Padding(10);
            this.tpgDefectInspection.Size = new System.Drawing.Size(465, 134);
            this.tpgDefectInspection.Text = "DEFECTINSPECTION";
            // 
            // grdInspectionItem
            // 
            this.grdInspectionItem.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionItem.IsUsePaging = false;
            this.grdInspectionItem.LanguageKey = null;
            this.grdInspectionItem.Location = new System.Drawing.Point(10, 10);
            this.grdInspectionItem.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionItem.Name = "grdInspectionItem";
            this.grdInspectionItem.ShowBorder = false;
            this.grdInspectionItem.ShowButtonBar = false;
            this.grdInspectionItem.ShowStatusBar = false;
            this.grdInspectionItem.Size = new System.Drawing.Size(445, 114);
            this.grdInspectionItem.TabIndex = 0;
            // 
            // tpgMeasureInspection
            // 
            this.tpgMeasureInspection.Controls.Add(this.grdInspectionItemSpec);
            this.tabInspectionResult.SetLanguageKey(this.tpgMeasureInspection, "MEASUREINSPECTION");
            this.tpgMeasureInspection.Name = "tpgMeasureInspection";
            this.tpgMeasureInspection.Padding = new System.Windows.Forms.Padding(10);
            this.tpgMeasureInspection.Size = new System.Drawing.Size(465, 134);
            this.tpgMeasureInspection.Text = "MEASUREINSPECTION";
            // 
            // grdInspectionItemSpec
            // 
            this.grdInspectionItemSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionItemSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionItemSpec.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionItemSpec.IsUsePaging = false;
            this.grdInspectionItemSpec.LanguageKey = null;
            this.grdInspectionItemSpec.Location = new System.Drawing.Point(10, 10);
            this.grdInspectionItemSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionItemSpec.Name = "grdInspectionItemSpec";
            this.grdInspectionItemSpec.ShowBorder = false;
            this.grdInspectionItemSpec.ShowButtonBar = false;
            this.grdInspectionItemSpec.ShowStatusBar = false;
            this.grdInspectionItemSpec.Size = new System.Drawing.Size(445, 114);
            this.grdInspectionItemSpec.TabIndex = 0;
            // 
            // spcRawAssy
            // 
            this.spcRawAssy.Dock = System.Windows.Forms.DockStyle.Top;
            this.spcRawAssy.Location = new System.Drawing.Point(0, 195);
            this.spcRawAssy.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcRawAssy.Name = "spcRawAssy";
            this.spcRawAssy.Size = new System.Drawing.Size(475, 5);
            this.spcRawAssy.TabIndex = 0;
            this.spcRawAssy.TabStop = false;
            // 
            // grdInspectionResult
            // 
            this.grdInspectionResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionResult.IsUsePaging = false;
            this.grdInspectionResult.LanguageKey = "SUBASSEMBLYIMPORTINSPECTIONRESULT";
            this.grdInspectionResult.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionResult.Name = "grdInspectionResult";
            this.grdInspectionResult.ShowBorder = true;
            this.grdInspectionResult.Size = new System.Drawing.Size(475, 195);
            this.grdInspectionResult.TabIndex = 1;
            // 
            // SubassemblyImportInspectionResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SubassemblyImportInspectionResult";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.spcTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInspItem)).EndInit();
            this.grpInspItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionResult)).EndInit();
            this.tabInspectionResult.ResumeLayout(false);
            this.tpgDefectInspection.ResumeLayout(false);
            this.tpgMeasureInspection.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartButton btnRegister;
        private Framework.SmartControls.SmartSplitTableLayoutPanel spcTable;
        private Framework.SmartControls.SmartSpliterControl spcRawAssy;
        private Framework.SmartControls.SmartBandedGrid grdInspectionResult;
        private Framework.SmartControls.SmartGroupBox grpInspItem;
        private Framework.SmartControls.SmartTabControl tabInspectionResult;
        private DevExpress.XtraTab.XtraTabPage tpgDefectInspection;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItem;
        private DevExpress.XtraTab.XtraTabPage tpgMeasureInspection;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemSpec;
    }
}