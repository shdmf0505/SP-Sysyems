namespace Micube.SmartMES.QualityAnalysis.Popup
{
    partial class ChemicalissuePopup
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
            this.gbxIssueInformation = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtReasonCodeId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtAnalysisDate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtEquipmentName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtChemicalLevel = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtDegree = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProcesssegmentclassName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtChildEquipmentName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtManagementScope = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtChemicalName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtAnalysisValue = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.ncrProgressControl = new Micube.SmartMES.QualityAnalysis.NCRProgressControl();
            this.issueRegistrationControl = new Micube.SmartMES.QualityAnalysis.IssueRegistrationControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxIssueInformation)).BeginInit();
            this.gbxIssueInformation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonCodeId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalysisDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChemicalLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDegree.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcesssegmentclassName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChildEquipmentName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManagementScope.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChemicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalysisValue.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1234, 798);
            // 
            // gbxIssueInformation
            // 
            this.gbxIssueInformation.Controls.Add(this.tableLayoutPanel1);
            this.gbxIssueInformation.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxIssueInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxIssueInformation.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.gbxIssueInformation.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxIssueInformation.Location = new System.Drawing.Point(0, 0);
            this.gbxIssueInformation.Margin = new System.Windows.Forms.Padding(0);
            this.gbxIssueInformation.Name = "gbxIssueInformation";
            this.gbxIssueInformation.ShowBorder = true;
            this.gbxIssueInformation.Size = new System.Drawing.Size(1234, 127);
            this.gbxIssueInformation.TabIndex = 8;
            this.gbxIssueInformation.Text = "이상발생 정보";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.txtReasonCodeId, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtAnalysisDate, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtEquipmentName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtChemicalLevel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDegree, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtProcesssegmentclassName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtChildEquipmentName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtManagementScope, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtChemicalName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtAnalysisValue, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1230, 94);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtReasonCodeId
            // 
            this.txtReasonCodeId.AutoHeight = false;
            this.txtReasonCodeId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReasonCodeId.LabelText = "이상발생사유";
            this.txtReasonCodeId.LanguageKey = "ABNORMALREASONCODE";
            this.txtReasonCodeId.Location = new System.Drawing.Point(3, 72);
            this.txtReasonCodeId.Name = "txtReasonCodeId";
            this.txtReasonCodeId.Properties.AutoHeight = false;
            this.txtReasonCodeId.Properties.ReadOnly = true;
            this.txtReasonCodeId.Size = new System.Drawing.Size(403, 19);
            this.txtReasonCodeId.TabIndex = 9;
            // 
            // txtAnalysisDate
            // 
            this.txtAnalysisDate.AutoHeight = false;
            this.txtAnalysisDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAnalysisDate.LabelText = "분석일";
            this.txtAnalysisDate.LanguageKey = "ANALYSISDATE";
            this.txtAnalysisDate.Location = new System.Drawing.Point(3, 3);
            this.txtAnalysisDate.Name = "txtAnalysisDate";
            this.txtAnalysisDate.Properties.AutoHeight = false;
            this.txtAnalysisDate.Properties.ReadOnly = true;
            this.txtAnalysisDate.Size = new System.Drawing.Size(403, 17);
            this.txtAnalysisDate.TabIndex = 0;
            // 
            // txtEquipmentName
            // 
            this.txtEquipmentName.AutoHeight = false;
            this.txtEquipmentName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEquipmentName.LabelText = "설비(호기)";
            this.txtEquipmentName.LanguageKey = "EQUIPMENT";
            this.txtEquipmentName.Location = new System.Drawing.Point(412, 3);
            this.txtEquipmentName.Name = "txtEquipmentName";
            this.txtEquipmentName.Properties.AutoHeight = false;
            this.txtEquipmentName.Properties.ReadOnly = true;
            this.txtEquipmentName.Size = new System.Drawing.Size(404, 17);
            this.txtEquipmentName.TabIndex = 1;
            // 
            // txtChemicalLevel
            // 
            this.txtChemicalLevel.AutoHeight = false;
            this.txtChemicalLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChemicalLevel.LabelText = "약품등급";
            this.txtChemicalLevel.LanguageKey = "CHEMICALLEVEL";
            this.txtChemicalLevel.Location = new System.Drawing.Point(822, 3);
            this.txtChemicalLevel.Name = "txtChemicalLevel";
            this.txtChemicalLevel.Properties.AutoHeight = false;
            this.txtChemicalLevel.Properties.ReadOnly = true;
            this.txtChemicalLevel.Size = new System.Drawing.Size(405, 17);
            this.txtChemicalLevel.TabIndex = 2;
            // 
            // txtDegree
            // 
            this.txtDegree.AutoHeight = false;
            this.txtDegree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDegree.LabelText = "회차";
            this.txtDegree.LanguageKey = "ROUND";
            this.txtDegree.Location = new System.Drawing.Point(3, 26);
            this.txtDegree.Name = "txtDegree";
            this.txtDegree.Properties.AutoHeight = false;
            this.txtDegree.Properties.ReadOnly = true;
            this.txtDegree.Size = new System.Drawing.Size(403, 17);
            this.txtDegree.TabIndex = 3;
            // 
            // txtProcesssegmentclassName
            // 
            this.txtProcesssegmentclassName.AutoHeight = false;
            this.txtProcesssegmentclassName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProcesssegmentclassName.LabelText = "대공정";
            this.txtProcesssegmentclassName.LanguageKey = "LARGEPROCESSSEGMENT";
            this.txtProcesssegmentclassName.Location = new System.Drawing.Point(3, 49);
            this.txtProcesssegmentclassName.Name = "txtProcesssegmentclassName";
            this.txtProcesssegmentclassName.Properties.AutoHeight = false;
            this.txtProcesssegmentclassName.Properties.ReadOnly = true;
            this.txtProcesssegmentclassName.Size = new System.Drawing.Size(403, 17);
            this.txtProcesssegmentclassName.TabIndex = 4;
            // 
            // txtChildEquipmentName
            // 
            this.txtChildEquipmentName.AutoHeight = false;
            this.txtChildEquipmentName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChildEquipmentName.LabelText = "설비단";
            this.txtChildEquipmentName.LanguageKey = "CHILDEQUIPMENT";
            this.txtChildEquipmentName.Location = new System.Drawing.Point(412, 26);
            this.txtChildEquipmentName.Name = "txtChildEquipmentName";
            this.txtChildEquipmentName.Properties.AutoHeight = false;
            this.txtChildEquipmentName.Properties.ReadOnly = true;
            this.txtChildEquipmentName.Size = new System.Drawing.Size(404, 17);
            this.txtChildEquipmentName.TabIndex = 5;
            // 
            // txtManagementScope
            // 
            this.txtManagementScope.AutoHeight = false;
            this.txtManagementScope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtManagementScope.LabelText = "규격범위";
            this.txtManagementScope.LanguageKey = "SPECRANGE";
            this.txtManagementScope.Location = new System.Drawing.Point(822, 26);
            this.txtManagementScope.Name = "txtManagementScope";
            this.txtManagementScope.Properties.AutoHeight = false;
            this.txtManagementScope.Properties.ReadOnly = true;
            this.txtManagementScope.Size = new System.Drawing.Size(405, 17);
            this.txtManagementScope.TabIndex = 6;
            // 
            // txtChemicalName
            // 
            this.txtChemicalName.AutoHeight = false;
            this.txtChemicalName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChemicalName.LabelText = "약품명";
            this.txtChemicalName.LanguageKey = "CHEMICALNAME";
            this.txtChemicalName.Location = new System.Drawing.Point(412, 49);
            this.txtChemicalName.Name = "txtChemicalName";
            this.txtChemicalName.Properties.AutoHeight = false;
            this.txtChemicalName.Properties.ReadOnly = true;
            this.txtChemicalName.Size = new System.Drawing.Size(404, 17);
            this.txtChemicalName.TabIndex = 7;
            // 
            // txtAnalysisValue
            // 
            this.txtAnalysisValue.AutoHeight = false;
            this.txtAnalysisValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAnalysisValue.LabelText = "분석치";
            this.txtAnalysisValue.LanguageKey = "ANALYSISVALUE";
            this.txtAnalysisValue.Location = new System.Drawing.Point(822, 49);
            this.txtAnalysisValue.Name = "txtAnalysisValue";
            this.txtAnalysisValue.Properties.AutoHeight = false;
            this.txtAnalysisValue.Properties.DisplayFormat.FormatString = "#.000";
            this.txtAnalysisValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAnalysisValue.Properties.ReadOnly = true;
            this.txtAnalysisValue.Size = new System.Drawing.Size(405, 17);
            this.txtAnalysisValue.TabIndex = 8;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.gbxIssueInformation, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.ncrProgressControl, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.issueRegistrationControl, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 6);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 7;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.27605F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.87112F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.85284F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1234, 798);
            this.smartSplitTableLayoutPanel1.TabIndex = 8;
            // 
            // ncrProgressControl
            // 
            this.ncrProgressControl.CurrentDataRow = null;
            this.ncrProgressControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ncrProgressControl.Location = new System.Drawing.Point(0, 137);
            this.ncrProgressControl.Margin = new System.Windows.Forms.Padding(0);
            this.ncrProgressControl.Name = "ncrProgressControl";
            this.ncrProgressControl.Size = new System.Drawing.Size(1234, 286);
            this.ncrProgressControl.TabIndex = 9;
            // 
            // issueRegistrationControl
            // 
            this.issueRegistrationControl.abnormalType = null;
            this.issueRegistrationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.issueRegistrationControl.isShowReasonCombo = false;
            this.issueRegistrationControl.Location = new System.Drawing.Point(0, 433);
            this.issueRegistrationControl.Margin = new System.Windows.Forms.Padding(0);
            this.issueRegistrationControl.Name = "issueRegistrationControl";
            this.issueRegistrationControl.ParentDataRow = null;
            this.issueRegistrationControl.ReasonTableVisible = true;
            this.issueRegistrationControl.Size = new System.Drawing.Size(1234, 322);
            this.issueRegistrationControl.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 765);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1234, 33);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.Location = new System.Drawing.Point(1151, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.Location = new System.Drawing.Point(1065, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // ChemicalissuePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 818);
            this.Name = "ChemicalissuePopup";
            this.Text = "ChemicalissuePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxIssueInformation)).EndInit();
            this.gbxIssueInformation.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonCodeId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalysisDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChemicalLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDegree.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcesssegmentclassName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChildEquipmentName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManagementScope.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChemicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalysisValue.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox gbxIssueInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartLabelTextBox txtAnalysisDate;
        private Framework.SmartControls.SmartLabelTextBox txtEquipmentName;
        private Framework.SmartControls.SmartLabelTextBox txtChemicalLevel;
        private Framework.SmartControls.SmartLabelTextBox txtDegree;
        private Framework.SmartControls.SmartLabelTextBox txtProcesssegmentclassName;
        private Framework.SmartControls.SmartLabelTextBox txtChildEquipmentName;
        private Framework.SmartControls.SmartLabelTextBox txtManagementScope;
        private Framework.SmartControls.SmartLabelTextBox txtChemicalName;
        private Framework.SmartControls.SmartLabelTextBox txtAnalysisValue;
        private NCRProgressControl ncrProgressControl;
        private IssueRegistrationControl issueRegistrationControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartLabelTextBox txtReasonCodeId;
        public Framework.SmartControls.SmartButton btnSave;
    }
}