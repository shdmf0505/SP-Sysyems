namespace Micube.SmartMES.QualityAnalysis
{
    partial class RawMaterialImportInspectionAbnormalOccurrence
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
            this.tabAbnormal = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgRawMaterial = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpInspItem = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tabInspectionResult = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDefectInspection = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgMeasureInspection = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdRawMaterialAbnormal = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgRawAssy = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpInspectionResultRegister = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemRawAssy = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemSpecRawAssy = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdRawAssyAbnormal = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAbnormal)).BeginInit();
            this.tabAbnormal.SuspendLayout();
            this.tpgRawMaterial.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInspItem)).BeginInit();
            this.grpInspItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionResult)).BeginInit();
            this.tabInspectionResult.SuspendLayout();
            this.tpgDefectInspection.SuspendLayout();
            this.tpgMeasureInspection.SuspendLayout();
            this.tpgRawAssy.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
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
            this.pnlCondition.Size = new System.Drawing.Size(296, 515);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Size = new System.Drawing.Size(556, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabAbnormal);
            this.pnlContent.Size = new System.Drawing.Size(556, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(861, 548);
            // 
            // tabAbnormal
            // 
            this.tabAbnormal.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabAbnormal.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAbnormal.Location = new System.Drawing.Point(0, 0);
            this.tabAbnormal.Name = "tabAbnormal";
            this.tabAbnormal.SelectedTabPage = this.tpgRawMaterial;
            this.tabAbnormal.Size = new System.Drawing.Size(556, 519);
            this.tabAbnormal.TabIndex = 0;
            this.tabAbnormal.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgRawMaterial,
            this.tpgRawAssy});
            // 
            // tpgRawMaterial
            // 
            this.tpgRawMaterial.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabAbnormal.SetLanguageKey(this.tpgRawMaterial, "RAWMATERIAL");
            this.tpgRawMaterial.Name = "tpgRawMaterial";
            this.tpgRawMaterial.Padding = new System.Windows.Forms.Padding(10);
            this.tpgRawMaterial.Size = new System.Drawing.Size(550, 490);
            this.tpgRawMaterial.Text = "RAWMATERIAL";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grpInspItem, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdRawMaterialAbnormal, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(530, 470);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // grpInspItem
            // 
            this.grpInspItem.Controls.Add(this.tabInspectionResult);
            this.grpInspItem.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpInspItem.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspItem.LanguageKey = "INSPECTIONRESULT";
            this.grpInspItem.Location = new System.Drawing.Point(0, 240);
            this.grpInspItem.Margin = new System.Windows.Forms.Padding(0);
            this.grpInspItem.Name = "grpInspItem";
            this.grpInspItem.ShowBorder = true;
            this.grpInspItem.Size = new System.Drawing.Size(530, 230);
            this.grpInspItem.TabIndex = 9;
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
            this.tabInspectionResult.Size = new System.Drawing.Size(526, 197);
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
            this.tpgDefectInspection.Size = new System.Drawing.Size(520, 168);
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
            this.grdInspectionItem.Size = new System.Drawing.Size(500, 148);
            this.grdInspectionItem.TabIndex = 0;
            // 
            // tpgMeasureInspection
            // 
            this.tpgMeasureInspection.Controls.Add(this.grdInspectionItemSpec);
            this.tabInspectionResult.SetLanguageKey(this.tpgMeasureInspection, "MEASUREINSPECTION");
            this.tpgMeasureInspection.Name = "tpgMeasureInspection";
            this.tpgMeasureInspection.Padding = new System.Windows.Forms.Padding(10);
            this.tpgMeasureInspection.Size = new System.Drawing.Size(439, 109);
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
            this.grdInspectionItemSpec.Size = new System.Drawing.Size(419, 89);
            this.grdInspectionItemSpec.TabIndex = 0;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 230);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(530, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdRawMaterialAbnormal
            // 
            this.grdRawMaterialAbnormal.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRawMaterialAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawMaterialAbnormal.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRawMaterialAbnormal.IsUsePaging = false;
            this.grdRawMaterialAbnormal.LanguageKey = "RAWMATERIALABNORMALOCCURRENCE";
            this.grdRawMaterialAbnormal.Location = new System.Drawing.Point(0, 0);
            this.grdRawMaterialAbnormal.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawMaterialAbnormal.Name = "grdRawMaterialAbnormal";
            this.grdRawMaterialAbnormal.ShowBorder = true;
            this.grdRawMaterialAbnormal.ShowStatusBar = false;
            this.grdRawMaterialAbnormal.Size = new System.Drawing.Size(530, 230);
            this.grdRawMaterialAbnormal.TabIndex = 1;
            // 
            // tpgRawAssy
            // 
            this.tpgRawAssy.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.tabAbnormal.SetLanguageKey(this.tpgRawAssy, "RAWASSY");
            this.tpgRawAssy.Name = "tpgRawAssy";
            this.tpgRawAssy.Padding = new System.Windows.Forms.Padding(10);
            this.tpgRawAssy.Size = new System.Drawing.Size(469, 372);
            this.tpgRawAssy.Text = "RAWASSY";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grpInspectionResultRegister, 0, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSpliterControl2, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdRawAssyAbnormal, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 3;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(449, 352);
            this.smartSplitTableLayoutPanel2.TabIndex = 2;
            // 
            // grpInspectionResultRegister
            // 
            this.grpInspectionResultRegister.Controls.Add(this.smartTabControl1);
            this.grpInspectionResultRegister.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspectionResultRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspectionResultRegister.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpInspectionResultRegister.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspectionResultRegister.LanguageKey = "INSPECTIONRESULT";
            this.grpInspectionResultRegister.Location = new System.Drawing.Point(0, 181);
            this.grpInspectionResultRegister.Margin = new System.Windows.Forms.Padding(0);
            this.grpInspectionResultRegister.Name = "grpInspectionResultRegister";
            this.grpInspectionResultRegister.ShowBorder = true;
            this.grpInspectionResultRegister.Size = new System.Drawing.Size(449, 171);
            this.grpInspectionResultRegister.TabIndex = 8;
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
            this.smartTabControl1.Size = new System.Drawing.Size(445, 138);
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
            this.xtraTabPage1.Size = new System.Drawing.Size(439, 109);
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
            this.grdInspectionItemRawAssy.Size = new System.Drawing.Size(419, 89);
            this.grdInspectionItemRawAssy.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdInspectionItemSpecRawAssy);
            this.smartTabControl1.SetLanguageKey(this.xtraTabPage2, "MEASUREINSPECTION");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(10);
            this.xtraTabPage2.Size = new System.Drawing.Size(439, 109);
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
            this.grdInspectionItemSpecRawAssy.Size = new System.Drawing.Size(419, 89);
            this.grdInspectionItemSpecRawAssy.TabIndex = 0;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 171);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(449, 5);
            this.smartSpliterControl2.TabIndex = 0;
            this.smartSpliterControl2.TabStop = false;
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
            this.grdRawAssyAbnormal.LanguageKey = "RAWASSYABNORMALOCCURRENCE";
            this.grdRawAssyAbnormal.Location = new System.Drawing.Point(0, 0);
            this.grdRawAssyAbnormal.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawAssyAbnormal.Name = "grdRawAssyAbnormal";
            this.grdRawAssyAbnormal.ShowBorder = true;
            this.grdRawAssyAbnormal.ShowStatusBar = false;
            this.grdRawAssyAbnormal.Size = new System.Drawing.Size(449, 171);
            this.grdRawAssyAbnormal.TabIndex = 1;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(476, -2);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 5;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // RawMaterialImportInspectionAbnormalOccurrence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 568);
            this.Name = "RawMaterialImportInspectionAbnormalOccurrence";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAbnormal)).EndInit();
            this.tabAbnormal.ResumeLayout(false);
            this.tpgRawMaterial.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInspItem)).EndInit();
            this.grpInspItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionResult)).EndInit();
            this.tabInspectionResult.ResumeLayout(false);
            this.tpgDefectInspection.ResumeLayout(false);
            this.tpgMeasureInspection.ResumeLayout(false);
            this.tpgRawAssy.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
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

        private Framework.SmartControls.SmartTabControl tabAbnormal;
        private DevExpress.XtraTab.XtraTabPage tpgRawMaterial;
        private DevExpress.XtraTab.XtraTabPage tpgRawAssy;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdRawMaterialAbnormal;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartBandedGrid grdRawAssyAbnormal;
        private Framework.SmartControls.SmartGroupBox grpInspItem;
        private Framework.SmartControls.SmartTabControl tabInspectionResult;
        private DevExpress.XtraTab.XtraTabPage tpgDefectInspection;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItem;
        private DevExpress.XtraTab.XtraTabPage tpgMeasureInspection;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemSpec;
        private Framework.SmartControls.SmartGroupBox grpInspectionResultRegister;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemRawAssy;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemSpecRawAssy;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}