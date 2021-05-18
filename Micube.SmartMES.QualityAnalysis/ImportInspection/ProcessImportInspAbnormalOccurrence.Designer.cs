namespace Micube.SmartMES.QualityAnalysis
{
    partial class ProcessImportInspAbnormalOccurrence
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
            this.spcAbnormal = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grpInspection = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tabInspection = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgExterior = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.grdInspectionItem = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.cboStandardType = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.tpgMeasure = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.grdMeasuredValue = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInspectionItemSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgDefect = new DevExpress.XtraTab.XtraTabPage();
            this.grdDefect = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdAbnormal = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnAgree = new Micube.Framework.SmartControls.SmartButton();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.spcAbnormal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInspection)).BeginInit();
            this.grpInspection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInspection)).BeginInit();
            this.tabInspection.SuspendLayout();
            this.tpgExterior.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStandardType.Properties)).BeginInit();
            this.tpgMeasure.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tpgDefect.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 468);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Controls.Add(this.btnAgree);
            this.pnlToolbar.Size = new System.Drawing.Size(769, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnAgree, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.spcAbnormal);
            this.pnlContent.Size = new System.Drawing.Size(769, 472);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1074, 501);
            // 
            // spcAbnormal
            // 
            this.spcAbnormal.ColumnCount = 1;
            this.spcAbnormal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.spcAbnormal.Controls.Add(this.grpInspection, 0, 2);
            this.spcAbnormal.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.spcAbnormal.Controls.Add(this.grdAbnormal, 0, 0);
            this.spcAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcAbnormal.Location = new System.Drawing.Point(0, 0);
            this.spcAbnormal.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcAbnormal.Name = "spcAbnormal";
            this.spcAbnormal.RowCount = 3;
            this.spcAbnormal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcAbnormal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.spcAbnormal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcAbnormal.Size = new System.Drawing.Size(769, 472);
            this.spcAbnormal.TabIndex = 0;
            // 
            // grpInspection
            // 
            this.grpInspection.Controls.Add(this.tabInspection);
            this.grpInspection.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspection.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grpInspection.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspection.LanguageKey = "INSPECTIONRESULT";
            this.grpInspection.Location = new System.Drawing.Point(0, 241);
            this.grpInspection.Margin = new System.Windows.Forms.Padding(0);
            this.grpInspection.Name = "grpInspection";
            this.grpInspection.ShowBorder = true;
            this.grpInspection.Size = new System.Drawing.Size(769, 231);
            this.grpInspection.TabIndex = 6;
            this.grpInspection.Text = "INSPECTIONRESULTREGISTER";
            // 
            // tabInspection
            // 
            this.tabInspection.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tabInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInspection.Location = new System.Drawing.Point(2, 31);
            this.tabInspection.Name = "tabInspection";
            this.tabInspection.SelectedTabPage = this.tpgExterior;
            this.tabInspection.Size = new System.Drawing.Size(765, 198);
            this.tabInspection.TabIndex = 0;
            this.tabInspection.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgExterior,
            this.tpgMeasure,
            this.tpgDefect});
            // 
            // tpgExterior
            // 
            this.tpgExterior.Controls.Add(this.tableLayoutPanel4);
            this.tabInspection.SetLanguageKey(this.tpgExterior, "EXTERIORINSPECTION");
            this.tpgExterior.Name = "tpgExterior";
            this.tpgExterior.Padding = new System.Windows.Forms.Padding(10);
            this.tpgExterior.Size = new System.Drawing.Size(759, 169);
            this.tpgExterior.Text = "EXTERIORINSPECTION";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.grdInspectionItem, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.cboStandardType, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(739, 149);
            this.tableLayoutPanel4.TabIndex = 1;
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
            this.grdInspectionItem.Location = new System.Drawing.Point(0, 25);
            this.grdInspectionItem.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionItem.Name = "grdInspectionItem";
            this.grdInspectionItem.ShowBorder = true;
            this.grdInspectionItem.ShowButtonBar = false;
            this.grdInspectionItem.ShowStatusBar = false;
            this.grdInspectionItem.Size = new System.Drawing.Size(739, 124);
            this.grdInspectionItem.TabIndex = 5;
            // 
            // cboStandardType
            // 
            this.cboStandardType.LabelText = "판정기준(외관검사)";
            this.cboStandardType.LanguageKey = "EXTINSPSTANDARDTYPE";
            this.cboStandardType.Location = new System.Drawing.Point(0, 0);
            this.cboStandardType.Margin = new System.Windows.Forms.Padding(0);
            this.cboStandardType.Name = "cboStandardType";
            this.cboStandardType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStandardType.Properties.NullText = "";
            this.cboStandardType.Properties.ReadOnly = true;
            this.cboStandardType.Properties.UseReadOnlyAppearance = false;
            this.cboStandardType.Size = new System.Drawing.Size(275, 20);
            this.cboStandardType.TabIndex = 4;
            // 
            // tpgMeasure
            // 
            this.tpgMeasure.Controls.Add(this.tableLayoutPanel6);
            this.tabInspection.SetLanguageKey(this.tpgMeasure, "MEASUREINSPECTION");
            this.tpgMeasure.Name = "tpgMeasure";
            this.tpgMeasure.Padding = new System.Windows.Forms.Padding(10);
            this.tpgMeasure.Size = new System.Drawing.Size(465, 134);
            this.tpgMeasure.Text = "MEASUREINSPECTION";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.grdMeasuredValue, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.grdInspectionItemSpec, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(445, 114);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // grdMeasuredValue
            // 
            this.grdMeasuredValue.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMeasuredValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMeasuredValue.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMeasuredValue.IsUsePaging = false;
            this.grdMeasuredValue.LanguageKey = "MEASUREVALUE";
            this.grdMeasuredValue.Location = new System.Drawing.Point(0, 26);
            this.grdMeasuredValue.Margin = new System.Windows.Forms.Padding(0);
            this.grdMeasuredValue.Name = "grdMeasuredValue";
            this.grdMeasuredValue.ShowBorder = true;
            this.grdMeasuredValue.ShowButtonBar = false;
            this.grdMeasuredValue.ShowStatusBar = false;
            this.grdMeasuredValue.Size = new System.Drawing.Size(445, 88);
            this.grdMeasuredValue.TabIndex = 2;
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
            this.grdInspectionItemSpec.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionItemSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionItemSpec.Name = "grdInspectionItemSpec";
            this.grdInspectionItemSpec.ShowBorder = true;
            this.grdInspectionItemSpec.ShowButtonBar = false;
            this.grdInspectionItemSpec.ShowStatusBar = false;
            this.grdInspectionItemSpec.Size = new System.Drawing.Size(445, 21);
            this.grdInspectionItemSpec.TabIndex = 1;
            // 
            // tpgDefect
            // 
            this.tpgDefect.Controls.Add(this.grdDefect);
            this.tabInspection.SetLanguageKey(this.tpgDefect, "DEFECTDISPOSAL");
            this.tpgDefect.Name = "tpgDefect";
            this.tpgDefect.Padding = new System.Windows.Forms.Padding(10);
            this.tpgDefect.Size = new System.Drawing.Size(465, 134);
            this.tpgDefect.Text = "DEFECTDISPOSAL";
            // 
            // grdDefect
            // 
            this.grdDefect.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefect.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefect.IsUsePaging = false;
            this.grdDefect.LanguageKey = null;
            this.grdDefect.Location = new System.Drawing.Point(10, 10);
            this.grdDefect.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefect.Name = "grdDefect";
            this.grdDefect.ShowBorder = true;
            this.grdDefect.ShowButtonBar = false;
            this.grdDefect.ShowStatusBar = false;
            this.grdDefect.Size = new System.Drawing.Size(445, 114);
            this.grdDefect.TabIndex = 2;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 231);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(769, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdAbnormal
            // 
            this.grdAbnormal.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdAbnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAbnormal.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdAbnormal.IsUsePaging = false;
            this.grdAbnormal.LanguageKey = "PROCESSABNORMALOCCURRENCE";
            this.grdAbnormal.Location = new System.Drawing.Point(0, 0);
            this.grdAbnormal.Margin = new System.Windows.Forms.Padding(0);
            this.grdAbnormal.Name = "grdAbnormal";
            this.grdAbnormal.ShowBorder = true;
            this.grdAbnormal.Size = new System.Drawing.Size(769, 231);
            this.grdAbnormal.TabIndex = 1;
            // 
            // btnAgree
            // 
            this.btnAgree.AllowFocus = false;
            this.btnAgree.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAgree.IsBusy = false;
            this.btnAgree.IsWrite = false;
            this.btnAgree.LanguageKey = "COMPANYAGREE";
            this.btnAgree.Location = new System.Drawing.Point(684, 0);
            this.btnAgree.Margin = new System.Windows.Forms.Padding(0);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAgree.Size = new System.Drawing.Size(85, 24);
            this.btnAgree.TabIndex = 5;
            this.btnAgree.Text = "smartButton1";
            this.btnAgree.TooltipLanguageKey = "";
            this.btnAgree.Visible = false;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(601, 0);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 7;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // ProcessImportInspAbnormalOccurrence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 521);
            this.Name = "ProcessImportInspAbnormalOccurrence";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.spcAbnormal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInspection)).EndInit();
            this.grpInspection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInspection)).EndInit();
            this.tabInspection.ResumeLayout(false);
            this.tpgExterior.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStandardType.Properties)).EndInit();
            this.tpgMeasure.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tpgDefect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel spcAbnormal;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdAbnormal;
        private Framework.SmartControls.SmartGroupBox grpInspection;
        private Framework.SmartControls.SmartTabControl tabInspection;
        private DevExpress.XtraTab.XtraTabPage tpgExterior;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItem;
        private Framework.SmartControls.SmartLabelComboBox cboStandardType;
        private DevExpress.XtraTab.XtraTabPage tpgMeasure;
        private DevExpress.XtraTab.XtraTabPage tpgDefect;
        private Framework.SmartControls.SmartBandedGrid grdDefect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Framework.SmartControls.SmartBandedGrid grdMeasuredValue;
        private Framework.SmartControls.SmartBandedGrid grdInspectionItemSpec;
        private Framework.SmartControls.SmartButton btnAgree;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}