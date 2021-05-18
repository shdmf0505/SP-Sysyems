namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectSelectPopup
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
            this.pnlConditions = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.cbBBTDefect = new Micube.Framework.SmartControls.SmartCheckBox();
            this.cbAOIDefect = new Micube.Framework.SmartControls.SmartCheckBox();
            this.lcbDefectType = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.ltbQCProcess = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.ltbDefectName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.pnlGridGroup = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.gridTarget = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.gridSource = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.lrBtnDefectSelect = new Micube.SmartMES.Commons.Controls.ucDataLeftRightBtn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlConditions)).BeginInit();
            this.pnlConditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbBBTDefect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAOIDefect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcbDefectType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbQCProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbDefectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridGroup)).BeginInit();
            this.pnlGridGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlGridGroup);
            this.pnlMain.Controls.Add(this.pnlConditions);
            this.pnlMain.Size = new System.Drawing.Size(628, 431);
            // 
            // pnlConditions
            // 
            this.pnlConditions.Controls.Add(this.btnSearch);
            this.pnlConditions.Controls.Add(this.cbBBTDefect);
            this.pnlConditions.Controls.Add(this.cbAOIDefect);
            this.pnlConditions.Controls.Add(this.lcbDefectType);
            this.pnlConditions.Controls.Add(this.ltbQCProcess);
            this.pnlConditions.Controls.Add(this.ltbDefectName);
            this.pnlConditions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConditions.Location = new System.Drawing.Point(0, 0);
            this.pnlConditions.Name = "pnlConditions";
            this.pnlConditions.Size = new System.Drawing.Size(628, 58);
            this.pnlConditions.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(524, 19);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "검색";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // cbBBTDefect
            // 
            this.cbBBTDefect.EditValue = true;
            this.cbBBTDefect.Location = new System.Drawing.Point(356, 56);
            this.cbBBTDefect.Name = "cbBBTDefect";
            this.cbBBTDefect.Properties.Caption = "BBT 불량";
            this.cbBBTDefect.Size = new System.Drawing.Size(116, 19);
            this.cbBBTDefect.TabIndex = 4;
            this.cbBBTDefect.Visible = false;
            // 
            // cbAOIDefect
            // 
            this.cbAOIDefect.EditValue = true;
            this.cbAOIDefect.Location = new System.Drawing.Point(252, 56);
            this.cbAOIDefect.Name = "cbAOIDefect";
            this.cbAOIDefect.Properties.Caption = "AOI 불량";
            this.cbAOIDefect.Size = new System.Drawing.Size(98, 19);
            this.cbAOIDefect.TabIndex = 3;
            this.cbAOIDefect.Visible = false;
            // 
            // lcbDefectType
            // 
            this.lcbDefectType.LabelText = "불량유형";
            this.lcbDefectType.LanguageKey = null;
            this.lcbDefectType.Location = new System.Drawing.Point(17, 55);
            this.lcbDefectType.Name = "lcbDefectType";
            this.lcbDefectType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lcbDefectType.Properties.NullText = "";
            this.lcbDefectType.Size = new System.Drawing.Size(220, 20);
            this.lcbDefectType.TabIndex = 2;
            this.lcbDefectType.Visible = false;
            // 
            // ltbQCProcess
            // 
            this.ltbQCProcess.LabelText = "품질공정";
            this.ltbQCProcess.LanguageKey = "QCSEGMENT";
            this.ltbQCProcess.Location = new System.Drawing.Point(252, 19);
            this.ltbQCProcess.Name = "ltbQCProcess";
            this.ltbQCProcess.Size = new System.Drawing.Size(220, 20);
            this.ltbQCProcess.TabIndex = 1;
            // 
            // ltbDefectName
            // 
            this.ltbDefectName.LabelText = "불량명";
            this.ltbDefectName.LanguageKey = "DEFECTNAME";
            this.ltbDefectName.Location = new System.Drawing.Point(17, 19);
            this.ltbDefectName.Name = "ltbDefectName";
            this.ltbDefectName.Size = new System.Drawing.Size(220, 20);
            this.ltbDefectName.TabIndex = 0;
            // 
            // pnlGridGroup
            // 
            this.pnlGridGroup.Controls.Add(this.btnSave);
            this.pnlGridGroup.Controls.Add(this.gridTarget);
            this.pnlGridGroup.Controls.Add(this.gridSource);
            this.pnlGridGroup.Controls.Add(this.lrBtnDefectSelect);
            this.pnlGridGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridGroup.Location = new System.Drawing.Point(0, 58);
            this.pnlGridGroup.Name = "pnlGridGroup";
            this.pnlGridGroup.Size = new System.Drawing.Size(628, 373);
            this.pnlGridGroup.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.Location = new System.Drawing.Point(524, 342);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // gridTarget
            // 
            this.gridTarget.Caption = "";
            this.gridTarget.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.gridTarget.IsUsePaging = false;
            this.gridTarget.LanguageKey = null;
            this.gridTarget.Location = new System.Drawing.Point(350, 12);
            this.gridTarget.Margin = new System.Windows.Forms.Padding(0);
            this.gridTarget.Name = "gridTarget";
            this.gridTarget.ShowBorder = true;
            this.gridTarget.ShowButtonBar = false;
            this.gridTarget.ShowStatusBar = false;
            this.gridTarget.Size = new System.Drawing.Size(270, 320);
            this.gridTarget.TabIndex = 4;
            // 
            // gridSource
            // 
            this.gridSource.Caption = "";
            this.gridSource.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.gridSource.IsUsePaging = false;
            this.gridSource.LanguageKey = null;
            this.gridSource.Location = new System.Drawing.Point(8, 12);
            this.gridSource.Margin = new System.Windows.Forms.Padding(0);
            this.gridSource.Name = "gridSource";
            this.gridSource.ShowBorder = true;
            this.gridSource.ShowButtonBar = false;
            this.gridSource.ShowStatusBar = false;
            this.gridSource.Size = new System.Drawing.Size(270, 320);
            this.gridSource.TabIndex = 3;
            // 
            // lrBtnDefectSelect
            // 
            this.lrBtnDefectSelect.Location = new System.Drawing.Point(287, 12);
            this.lrBtnDefectSelect.Name = "lrBtnDefectSelect";
            this.lrBtnDefectSelect.Size = new System.Drawing.Size(54, 320);
            this.lrBtnDefectSelect.SourceGrid = null;
            this.lrBtnDefectSelect.TabIndex = 2;
            this.lrBtnDefectSelect.TargetGrid = null;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "gridBand1";
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = -1;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "gridBand1";
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = -1;
            // 
            // DefectSelectPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 451);
            this.LanguageKey = "SELECTDEFECTCODE";
            this.Name = "DefectSelectPopup";
            this.Text = "불량 선택";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlConditions)).EndInit();
            this.pnlConditions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbBBTDefect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAOIDefect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcbDefectType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbQCProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbDefectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridGroup)).EndInit();
            this.pnlGridGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlConditions;
        private Framework.SmartControls.SmartLabelTextBox ltbQCProcess;
        private Framework.SmartControls.SmartLabelTextBox ltbDefectName;
        private Framework.SmartControls.SmartCheckBox cbBBTDefect;
        private Framework.SmartControls.SmartCheckBox cbAOIDefect;
        private Framework.SmartControls.SmartLabelComboBox lcbDefectType;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartPanel pnlGridGroup;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private Commons.Controls.ucDataLeftRightBtn lrBtnDefectSelect;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private Framework.SmartControls.SmartBandedGrid gridTarget;
        private Framework.SmartControls.SmartBandedGrid gridSource;
        private Framework.SmartControls.SmartButton btnSave;
    }
}