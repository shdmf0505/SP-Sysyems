namespace Micube.SmartMES.QualityAnalysis
{
    partial class RawMaterialImportInspectionResult
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
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tabMaterialInspection = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgRawMaterial = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpInspItem = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tabInspectionResult = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDefectInspection = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgMeasureInspection = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMaterialInspectionResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgRawAssy = new DevExpress.XtraTab.XtraTabPage();
            this.spcRawAssy = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpInspectionResultRegister = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemRawAssy = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdInspectionItemSpecRawAssy = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdRawAssyInspectionResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spPageCount = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMaterialInspection)).BeginInit();
            this.tabMaterialInspection.SuspendLayout();
            this.tpgRawMaterial.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInspItem)).BeginInit();
            this.grpInspItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionResult)).BeginInit();
            this.tabInspectionResult.SuspendLayout();
            this.tpgDefectInspection.SuspendLayout();
            this.tpgMeasureInspection.SuspendLayout();
            this.tpgRawAssy.SuspendLayout();
            this.spcRawAssy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInspectionResultRegister)).BeginInit();
            this.grpInspectionResultRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spPageCount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 507);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Controls.Add(this.btnPrint);
            this.pnlToolbar.Size = new System.Drawing.Size(550, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPrint, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(550, 511);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(855, 540);
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrint.IsBusy = false;
            this.btnPrint.IsWrite = false;
            this.btnPrint.LanguageKey = "REPORTPRINTING";
            this.btnPrint.Location = new System.Drawing.Point(470, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrint.Size = new System.Drawing.Size(80, 24);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "smartButton1";
            this.btnPrint.TooltipLanguageKey = "";
            this.btnPrint.Visible = false;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(384, -1);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 6;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabMaterialInspection, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.spPageCount, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(550, 511);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // tabMaterialInspection
            // 
            this.tabMaterialInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMaterialInspection.Location = new System.Drawing.Point(0, 25);
            this.tabMaterialInspection.Margin = new System.Windows.Forms.Padding(0);
            this.tabMaterialInspection.Name = "tabMaterialInspection";
            this.tabMaterialInspection.SelectedTabPage = this.tpgRawMaterial;
            this.tabMaterialInspection.Size = new System.Drawing.Size(550, 486);
            this.tabMaterialInspection.TabIndex = 1;
            this.tabMaterialInspection.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgRawMaterial,
            this.tpgRawAssy});
            // 
            // tpgRawMaterial
            // 
            this.tpgRawMaterial.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.tabMaterialInspection.SetLanguageKey(this.tpgRawMaterial, "RAWMATERIAL");
            this.tpgRawMaterial.Name = "tpgRawMaterial";
            this.tpgRawMaterial.Padding = new System.Windows.Forms.Padding(10);
            this.tpgRawMaterial.Size = new System.Drawing.Size(544, 457);
            this.tpgRawMaterial.Text = "RAWMATERIAL";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grpInspItem, 0, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdMaterialInspectionResult, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 3;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(524, 437);
            this.smartSplitTableLayoutPanel2.TabIndex = 0;
            // 
            // grpInspItem
            // 
            this.grpInspItem.Controls.Add(this.tabInspectionResult);
            this.grpInspItem.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspItem.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpInspItem.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspItem.LanguageKey = "INSPECTIONRESULT";
            this.grpInspItem.Location = new System.Drawing.Point(0, 223);
            this.grpInspItem.Margin = new System.Windows.Forms.Padding(0);
            this.grpInspItem.Name = "grpInspItem";
            this.grpInspItem.ShowBorder = true;
            this.grpInspItem.Size = new System.Drawing.Size(524, 214);
            this.grpInspItem.TabIndex = 8;
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
            this.tabInspectionResult.Size = new System.Drawing.Size(520, 181);
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
            this.tpgDefectInspection.Size = new System.Drawing.Size(514, 152);
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
            this.grdInspectionItem.Size = new System.Drawing.Size(494, 132);
            this.grdInspectionItem.TabIndex = 0;
            this.grdInspectionItem.UseAutoBestFitColumns = false;
            // 
            // tpgMeasureInspection
            // 
            this.tpgMeasureInspection.Controls.Add(this.grdInspectionItemSpec);
            this.tabInspectionResult.SetLanguageKey(this.tpgMeasureInspection, "MEASUREINSPECTION");
            this.tpgMeasureInspection.Name = "tpgMeasureInspection";
            this.tpgMeasureInspection.Padding = new System.Windows.Forms.Padding(10);
            this.tpgMeasureInspection.Size = new System.Drawing.Size(514, 164);
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
            this.grdInspectionItemSpec.Size = new System.Drawing.Size(494, 144);
            this.grdInspectionItemSpec.TabIndex = 0;
            this.grdInspectionItemSpec.UseAutoBestFitColumns = false;
            // 
            // grdMaterialInspectionResult
            // 
            this.grdMaterialInspectionResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMaterialInspectionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterialInspectionResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaterialInspectionResult.IsUsePaging = false;
            this.grdMaterialInspectionResult.LanguageKey = "RAWMATERIALIMPORTINSPECTIONRESULT";
            this.grdMaterialInspectionResult.Location = new System.Drawing.Point(0, 0);
            this.grdMaterialInspectionResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterialInspectionResult.Name = "grdMaterialInspectionResult";
            this.grdMaterialInspectionResult.ShowBorder = true;
            this.grdMaterialInspectionResult.Size = new System.Drawing.Size(524, 213);
            this.grdMaterialInspectionResult.TabIndex = 9;
            this.grdMaterialInspectionResult.UseAutoBestFitColumns = false;
            // 
            // tpgRawAssy
            // 
            this.tpgRawAssy.Controls.Add(this.spcRawAssy);
            this.tabMaterialInspection.SetLanguageKey(this.tpgRawAssy, "RAWASSY");
            this.tpgRawAssy.Name = "tpgRawAssy";
            this.tpgRawAssy.Padding = new System.Windows.Forms.Padding(10);
            this.tpgRawAssy.Size = new System.Drawing.Size(544, 482);
            this.tpgRawAssy.Text = "RAWASSY";
            // 
            // spcRawAssy
            // 
            this.spcRawAssy.ColumnCount = 1;
            this.spcRawAssy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.spcRawAssy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.spcRawAssy.Controls.Add(this.grpInspectionResultRegister, 0, 2);
            this.spcRawAssy.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.spcRawAssy.Controls.Add(this.grdRawAssyInspectionResult, 0, 0);
            this.spcRawAssy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcRawAssy.Location = new System.Drawing.Point(10, 10);
            this.spcRawAssy.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcRawAssy.Name = "spcRawAssy";
            this.spcRawAssy.RowCount = 3;
            this.spcRawAssy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcRawAssy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.spcRawAssy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcRawAssy.Size = new System.Drawing.Size(524, 462);
            this.spcRawAssy.TabIndex = 0;
            // 
            // grpInspectionResultRegister
            // 
            this.grpInspectionResultRegister.Controls.Add(this.smartTabControl1);
            this.grpInspectionResultRegister.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspectionResultRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspectionResultRegister.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpInspectionResultRegister.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspectionResultRegister.LanguageKey = "INSPECTIONRESULT";
            this.grpInspectionResultRegister.Location = new System.Drawing.Point(0, 236);
            this.grpInspectionResultRegister.Margin = new System.Windows.Forms.Padding(0);
            this.grpInspectionResultRegister.Name = "grpInspectionResultRegister";
            this.grpInspectionResultRegister.ShowBorder = true;
            this.grpInspectionResultRegister.Size = new System.Drawing.Size(524, 226);
            this.grpInspectionResultRegister.TabIndex = 7;
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
            this.smartTabControl1.Size = new System.Drawing.Size(520, 193);
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
            this.xtraTabPage1.Size = new System.Drawing.Size(514, 164);
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
            this.grdInspectionItemRawAssy.Size = new System.Drawing.Size(494, 144);
            this.grdInspectionItemRawAssy.TabIndex = 0;
            this.grdInspectionItemRawAssy.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdInspectionItemSpecRawAssy);
            this.smartTabControl1.SetLanguageKey(this.xtraTabPage2, "MEASUREINSPECTION");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(10);
            this.xtraTabPage2.Size = new System.Drawing.Size(514, 164);
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
            this.grdInspectionItemSpecRawAssy.Size = new System.Drawing.Size(494, 144);
            this.grdInspectionItemSpecRawAssy.TabIndex = 0;
            this.grdInspectionItemSpecRawAssy.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 226);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(524, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdRawAssyInspectionResult
            // 
            this.grdRawAssyInspectionResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRawAssyInspectionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawAssyInspectionResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRawAssyInspectionResult.IsUsePaging = false;
            this.grdRawAssyInspectionResult.LanguageKey = "RAWASSYIMPORTINSPECTIONRESULT";
            this.grdRawAssyInspectionResult.Location = new System.Drawing.Point(0, 0);
            this.grdRawAssyInspectionResult.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawAssyInspectionResult.Name = "grdRawAssyInspectionResult";
            this.grdRawAssyInspectionResult.ShowBorder = true;
            this.grdRawAssyInspectionResult.ShowStatusBar = false;
            this.grdRawAssyInspectionResult.Size = new System.Drawing.Size(524, 226);
            this.grdRawAssyInspectionResult.TabIndex = 1;
            this.grdRawAssyInspectionResult.UseAutoBestFitColumns = false;
            // 
            // spPageCount
            // 
            this.spPageCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spPageCount.LabelText = "페이지수";
            this.spPageCount.LanguageKey = "PAGECOUNT";
            this.spPageCount.Location = new System.Drawing.Point(327, 3);
            this.spPageCount.Name = "spPageCount";
            this.spPageCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spPageCount.Size = new System.Drawing.Size(220, 20);
            this.spPageCount.TabIndex = 2;
            // 
            // RawMaterialImportInspectionResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 560);
            this.Name = "RawMaterialImportInspectionResult";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMaterialInspection)).EndInit();
            this.tabMaterialInspection.ResumeLayout(false);
            this.tpgRawMaterial.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInspItem)).EndInit();
            this.grpInspItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInspectionResult)).EndInit();
            this.tabInspectionResult.ResumeLayout(false);
            this.tpgDefectInspection.ResumeLayout(false);
            this.tpgMeasureInspection.ResumeLayout(false);
            this.tpgRawAssy.ResumeLayout(false);
            this.spcRawAssy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInspectionResultRegister)).EndInit();
            this.grpInspectionResultRegister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spPageCount.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartButton btnPrint;
        private Framework.SmartControls.SmartButton btnPopupFlag;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartTabControl tabMaterialInspection;
        private DevExpress.XtraTab.XtraTabPage tpgRawMaterial;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartGroupBox grpInspItem;
        private Framework.SmartControls.SmartTabControl tabInspectionResult;
        private DevExpress.XtraTab.XtraTabPage tpgDefectInspection;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItem;
        private DevExpress.XtraTab.XtraTabPage tpgMeasureInspection;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemSpec;
        private Framework.SmartControls.SmartBandedGrid grdMaterialInspectionResult;
        private DevExpress.XtraTab.XtraTabPage tpgRawAssy;
        private Framework.SmartControls.SmartSplitTableLayoutPanel spcRawAssy;
        private Framework.SmartControls.SmartGroupBox grpInspectionResultRegister;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemRawAssy;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemSpecRawAssy;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdRawAssyInspectionResult;
        private Framework.SmartControls.SmartLabelSpinEdit spPageCount;
    }
}