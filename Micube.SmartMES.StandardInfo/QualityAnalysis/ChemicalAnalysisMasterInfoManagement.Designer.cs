namespace Micube.SmartMES.StandardInfo
{
    partial class ChemicalAnalysisMasterInfoManagement
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
            this.tabChemicalWater = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgChemical = new DevExpress.XtraTab.XtraTabPage();
            this.spcChemicalInspection1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdEQUIPMENTSub = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnSpecRegisterDetail = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.grdInspectionitemrelList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgFormula = new DevExpress.XtraTab.XtraTabPage();
            this.spcFormula = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdFormulaDef = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdFormula = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabChemicalWater)).BeginInit();
            this.tabChemicalWater.SuspendLayout();
            this.tpgChemical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcChemicalInspection1)).BeginInit();
            this.spcChemicalInspection1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tpgFormula.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcFormula)).BeginInit();
            this.spcFormula.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 655);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(871, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabChemicalWater);
            this.pnlContent.Size = new System.Drawing.Size(871, 658);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1252, 694);
            // 
            // tabChemicalWater
            // 
            this.tabChemicalWater.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabChemicalWater.Location = new System.Drawing.Point(0, 0);
            this.tabChemicalWater.Name = "tabChemicalWater";
            this.tabChemicalWater.SelectedTabPage = this.tpgChemical;
            this.tabChemicalWater.Size = new System.Drawing.Size(871, 658);
            this.tabChemicalWater.TabIndex = 0;
            this.tabChemicalWater.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgChemical,
            this.tpgFormula});
            // 
            // tpgChemical
            // 
            this.tpgChemical.Controls.Add(this.spcChemicalInspection1);
            this.tabChemicalWater.SetLanguageKey(this.tpgChemical, "CHEMICALINSPECTION");
            this.tpgChemical.Name = "tpgChemical";
            this.tpgChemical.Padding = new System.Windows.Forms.Padding(10);
            this.tpgChemical.Size = new System.Drawing.Size(864, 622);
            this.tpgChemical.Text = "CHMICALINSPECTION";
            // 
            // spcChemicalInspection1
            // 
            this.spcChemicalInspection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcChemicalInspection1.Horizontal = false;
            this.spcChemicalInspection1.Location = new System.Drawing.Point(10, 10);
            this.spcChemicalInspection1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcChemicalInspection1.Name = "spcChemicalInspection1";
            this.spcChemicalInspection1.Panel1.Controls.Add(this.grdEQUIPMENTSub);
            this.spcChemicalInspection1.Panel1.Text = "Panel1";
            this.spcChemicalInspection1.Panel2.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.spcChemicalInspection1.Panel2.Text = "Panel2";
            this.spcChemicalInspection1.Size = new System.Drawing.Size(844, 602);
            this.spcChemicalInspection1.SplitterPosition = 347;
            this.spcChemicalInspection1.TabIndex = 0;
            this.spcChemicalInspection1.Text = "smartSpliterContainer1";
            // 
            // grdEQUIPMENTSub
            // 
            this.grdEQUIPMENTSub.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEQUIPMENTSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEQUIPMENTSub.IsUsePaging = false;
            this.grdEQUIPMENTSub.LanguageKey = "INSPITEMCLASSLIST";
            this.grdEQUIPMENTSub.Location = new System.Drawing.Point(0, 0);
            this.grdEQUIPMENTSub.Margin = new System.Windows.Forms.Padding(0);
            this.grdEQUIPMENTSub.Name = "grdEQUIPMENTSub";
            this.grdEQUIPMENTSub.ShowBorder = true;
            this.grdEQUIPMENTSub.ShowStatusBar = false;
            this.grdEQUIPMENTSub.Size = new System.Drawing.Size(844, 347);
            this.grdEQUIPMENTSub.TabIndex = 2;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSpecRegisterDetail, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(844, 249);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // btnSpecRegisterDetail
            // 
            this.btnSpecRegisterDetail.AllowFocus = false;
            this.btnSpecRegisterDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSpecRegisterDetail.IsBusy = false;
            this.btnSpecRegisterDetail.LanguageKey = "SPEC";
            this.btnSpecRegisterDetail.Location = new System.Drawing.Point(744, 0);
            this.btnSpecRegisterDetail.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSpecRegisterDetail.Name = "btnSpecRegisterDetail";
            this.btnSpecRegisterDetail.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSpecRegisterDetail.Size = new System.Drawing.Size(97, 33);
            this.btnSpecRegisterDetail.TabIndex = 6;
            this.btnSpecRegisterDetail.Text = "Spec";
            this.btnSpecRegisterDetail.TooltipLanguageKey = "";
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.grdInspectionitemrelList);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(3, 36);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(838, 210);
            this.smartPanel1.TabIndex = 0;
            // 
            // grdInspectionitemrelList
            // 
            this.grdInspectionitemrelList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionitemrelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionitemrelList.IsUsePaging = false;
            this.grdInspectionitemrelList.LanguageKey = "INSPECTIONITEMRELLIST";
            this.grdInspectionitemrelList.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionitemrelList.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionitemrelList.Name = "grdInspectionitemrelList";
            this.grdInspectionitemrelList.ShowBorder = true;
            this.grdInspectionitemrelList.ShowStatusBar = false;
            this.grdInspectionitemrelList.Size = new System.Drawing.Size(838, 210);
            this.grdInspectionitemrelList.TabIndex = 2;
            // 
            // tpgFormula
            // 
            this.tpgFormula.Controls.Add(this.spcFormula);
            this.tabChemicalWater.SetLanguageKey(this.tpgFormula, "FORMULA");
            this.tpgFormula.Name = "tpgFormula";
            this.tpgFormula.Padding = new System.Windows.Forms.Padding(10);
            this.tpgFormula.Size = new System.Drawing.Size(864, 622);
            this.tpgFormula.Text = "FORMULA";
            // 
            // spcFormula
            // 
            this.spcFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcFormula.Location = new System.Drawing.Point(10, 10);
            this.spcFormula.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcFormula.Name = "spcFormula";
            this.spcFormula.Panel1.Controls.Add(this.grdFormulaDef);
            this.spcFormula.Panel1.Text = "Panel1";
            this.spcFormula.Panel2.Controls.Add(this.grdFormula);
            this.spcFormula.Panel2.Text = "Panel2";
            this.spcFormula.Size = new System.Drawing.Size(844, 602);
            this.spcFormula.SplitterPosition = 650;
            this.spcFormula.TabIndex = 0;
            this.spcFormula.Text = "smartSpliterContainer1";
            // 
            // grdFormulaDef
            // 
            this.grdFormulaDef.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFormulaDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFormulaDef.IsUsePaging = false;
            this.grdFormulaDef.LanguageKey = "FORMULADEFLIST";
            this.grdFormulaDef.Location = new System.Drawing.Point(0, 0);
            this.grdFormulaDef.Margin = new System.Windows.Forms.Padding(0);
            this.grdFormulaDef.Name = "grdFormulaDef";
            this.grdFormulaDef.ShowBorder = true;
            this.grdFormulaDef.ShowStatusBar = false;
            this.grdFormulaDef.Size = new System.Drawing.Size(650, 602);
            this.grdFormulaDef.TabIndex = 0;
            // 
            // grdFormula
            // 
            this.grdFormula.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFormula.IsUsePaging = false;
            this.grdFormula.LanguageKey = "FORMULALIST";
            this.grdFormula.Location = new System.Drawing.Point(0, 0);
            this.grdFormula.Margin = new System.Windows.Forms.Padding(0);
            this.grdFormula.Name = "grdFormula";
            this.grdFormula.ShowBorder = true;
            this.grdFormula.ShowStatusBar = false;
            this.grdFormula.Size = new System.Drawing.Size(188, 602);
            this.grdFormula.TabIndex = 0;
            // 
            // ChemicalAnalysisMasterInfoManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 732);
            this.Name = "ChemicalAnalysisMasterInfoManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabChemicalWater)).EndInit();
            this.tabChemicalWater.ResumeLayout(false);
            this.tpgChemical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcChemicalInspection1)).EndInit();
            this.spcChemicalInspection1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tpgFormula.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcFormula)).EndInit();
            this.spcFormula.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabChemicalWater;
        private DevExpress.XtraTab.XtraTabPage tpgChemical;
        private DevExpress.XtraTab.XtraTabPage tpgFormula;
        private Framework.SmartControls.SmartSpliterContainer spcChemicalInspection1;
        private Framework.SmartControls.SmartSpliterContainer spcFormula;
        private Framework.SmartControls.SmartBandedGrid grdFormulaDef;
        private Framework.SmartControls.SmartBandedGrid grdFormula;
        private Framework.SmartControls.SmartBandedGrid grdEQUIPMENTSub;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartBandedGrid grdInspectionitemrelList;
        private Framework.SmartControls.SmartButton btnSpecRegisterDetail;
    }
}