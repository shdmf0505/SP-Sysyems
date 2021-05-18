namespace Micube.SmartMES.QualityAnalysis
{
    partial class ShipmentInspAbnormalOccurrencePopup
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.grpInfo = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDefectRate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtInspectionQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductdefName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtDescription = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtParentLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtDefectQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductdefId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtDefectCodeName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtReasonCode = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtNGCount = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtNCRIssueDate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.tabNCRPic = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgNCR = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.issueRegistrationControl = new Micube.SmartMES.QualityAnalysis.IssueRegistrationControl();
            this.ncrProgressControl = new Micube.SmartMES.QualityAnalysis.NCRProgressControl();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.tpgPic = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowMeasuredPicture = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDownload = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).BeginInit();
            this.grpInfo.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInspectionQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductdefName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductdefId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNGCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNCRIssueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabNCRPic)).BeginInit();
            this.tabNCRPic.SuspendLayout();
            this.tpgNCR.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.tpgPic.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1234, 833);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.grpInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabNCRPic, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1234, 833);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 808);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1234, 25);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(1154, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "smartButton2";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(1068, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "smartButton3";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.tableLayoutPanel2);
            this.grpInfo.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInfo.LanguageKey = "ABNORMALINFO";
            this.grpInfo.Location = new System.Drawing.Point(0, 0);
            this.grpInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.ShowBorder = true;
            this.grpInfo.Size = new System.Drawing.Size(1234, 198);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.Text = "smartGroupBox1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.txtDefectRate, 2, 8);
            this.tableLayoutPanel2.Controls.Add(this.txtInspectionQty, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtProductdefName, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtDescription, 2, 10);
            this.tableLayoutPanel2.Controls.Add(this.txtLotId, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtParentLotId, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDefectQty, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.txtProductdefId, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtDefectCodeName, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtReasonCode, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.txtNGCount, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtNCRIssueDate, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1230, 165);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtDefectRate
            // 
            this.txtDefectRate.AutoHeight = false;
            this.txtDefectRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDefectRate.LanguageKey = "DEFECTRATE";
            this.txtDefectRate.Location = new System.Drawing.Point(617, 112);
            this.txtDefectRate.Margin = new System.Windows.Forms.Padding(0);
            this.txtDefectRate.Name = "txtDefectRate";
            this.txtDefectRate.Properties.AutoHeight = false;
            this.txtDefectRate.Properties.ReadOnly = true;
            this.txtDefectRate.Properties.UseReadOnlyAppearance = false;
            this.txtDefectRate.Size = new System.Drawing.Size(613, 23);
            this.txtDefectRate.TabIndex = 11;
            // 
            // txtInspectionQty
            // 
            this.txtInspectionQty.AutoHeight = false;
            this.txtInspectionQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInspectionQty.LanguageKey = "INSPECTIONQTY";
            this.txtInspectionQty.Location = new System.Drawing.Point(617, 84);
            this.txtInspectionQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtInspectionQty.Name = "txtInspectionQty";
            this.txtInspectionQty.Properties.AutoHeight = false;
            this.txtInspectionQty.Properties.ReadOnly = true;
            this.txtInspectionQty.Properties.UseReadOnlyAppearance = false;
            this.txtInspectionQty.Size = new System.Drawing.Size(613, 23);
            this.txtInspectionQty.TabIndex = 10;
            // 
            // txtProductdefName
            // 
            this.txtProductdefName.AutoHeight = false;
            this.txtProductdefName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductdefName.LanguageKey = "PRODUCTDEFNAME";
            this.txtProductdefName.Location = new System.Drawing.Point(617, 56);
            this.txtProductdefName.Margin = new System.Windows.Forms.Padding(0);
            this.txtProductdefName.Name = "txtProductdefName";
            this.txtProductdefName.Properties.AutoHeight = false;
            this.txtProductdefName.Properties.ReadOnly = true;
            this.txtProductdefName.Properties.UseReadOnlyAppearance = false;
            this.txtProductdefName.Size = new System.Drawing.Size(613, 23);
            this.txtProductdefName.TabIndex = 9;
            // 
            // txtDescription
            // 
            this.txtDescription.AutoHeight = false;
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.LanguageKey = "DESCRIPTION";
            this.txtDescription.Location = new System.Drawing.Point(617, 140);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(0);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.AutoHeight = false;
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Properties.UseReadOnlyAppearance = false;
            this.txtDescription.Size = new System.Drawing.Size(613, 25);
            this.txtDescription.TabIndex = 8;
            // 
            // txtLotId
            // 
            this.txtLotId.AutoHeight = false;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(617, 28);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.AutoHeight = false;
            this.txtLotId.Properties.ReadOnly = true;
            this.txtLotId.Properties.UseReadOnlyAppearance = false;
            this.txtLotId.Size = new System.Drawing.Size(613, 23);
            this.txtLotId.TabIndex = 7;
            // 
            // txtParentLotId
            // 
            this.txtParentLotId.AutoHeight = false;
            this.txtParentLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtParentLotId.LanguageKey = "PARENTLOTID";
            this.txtParentLotId.Location = new System.Drawing.Point(617, 0);
            this.txtParentLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtParentLotId.Name = "txtParentLotId";
            this.txtParentLotId.Properties.AutoHeight = false;
            this.txtParentLotId.Properties.ReadOnly = true;
            this.txtParentLotId.Properties.UseReadOnlyAppearance = false;
            this.txtParentLotId.Size = new System.Drawing.Size(613, 23);
            this.txtParentLotId.TabIndex = 6;
            // 
            // txtDefectQty
            // 
            this.txtDefectQty.AutoHeight = false;
            this.txtDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDefectQty.LanguageKey = "DEFECTQTY";
            this.txtDefectQty.Location = new System.Drawing.Point(0, 112);
            this.txtDefectQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtDefectQty.Name = "txtDefectQty";
            this.txtDefectQty.Properties.AutoHeight = false;
            this.txtDefectQty.Size = new System.Drawing.Size(612, 23);
            this.txtDefectQty.TabIndex = 5;
            // 
            // txtProductdefId
            // 
            this.txtProductdefId.AutoHeight = false;
            this.txtProductdefId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductdefId.LanguageKey = "PRODUCTDEFID";
            this.txtProductdefId.Location = new System.Drawing.Point(0, 56);
            this.txtProductdefId.Margin = new System.Windows.Forms.Padding(0);
            this.txtProductdefId.Name = "txtProductdefId";
            this.txtProductdefId.Properties.AutoHeight = false;
            this.txtProductdefId.Properties.ReadOnly = true;
            this.txtProductdefId.Properties.UseReadOnlyAppearance = false;
            this.txtProductdefId.Size = new System.Drawing.Size(612, 23);
            this.txtProductdefId.TabIndex = 4;
            // 
            // txtDefectCodeName
            // 
            this.txtDefectCodeName.AutoHeight = false;
            this.txtDefectCodeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDefectCodeName.LanguageKey = "DEFECTCODENAME";
            this.txtDefectCodeName.Location = new System.Drawing.Point(0, 84);
            this.txtDefectCodeName.Margin = new System.Windows.Forms.Padding(0);
            this.txtDefectCodeName.Name = "txtDefectCodeName";
            this.txtDefectCodeName.Properties.AutoHeight = false;
            this.txtDefectCodeName.Properties.ReadOnly = true;
            this.txtDefectCodeName.Properties.UseReadOnlyAppearance = false;
            this.txtDefectCodeName.Size = new System.Drawing.Size(612, 23);
            this.txtDefectCodeName.TabIndex = 3;
            // 
            // txtReasonCode
            // 
            this.txtReasonCode.AutoHeight = false;
            this.txtReasonCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReasonCode.LanguageKey = "REASONCODENAME";
            this.txtReasonCode.Location = new System.Drawing.Point(0, 140);
            this.txtReasonCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtReasonCode.Name = "txtReasonCode";
            this.txtReasonCode.Properties.AutoHeight = false;
            this.txtReasonCode.Properties.ReadOnly = true;
            this.txtReasonCode.Properties.UseReadOnlyAppearance = false;
            this.txtReasonCode.Size = new System.Drawing.Size(612, 25);
            this.txtReasonCode.TabIndex = 2;
            // 
            // txtNGCount
            // 
            this.txtNGCount.AutoHeight = false;
            this.txtNGCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNGCount.LanguageKey = "NGCOUNT";
            this.txtNGCount.Location = new System.Drawing.Point(0, 28);
            this.txtNGCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtNGCount.Name = "txtNGCount";
            this.txtNGCount.Properties.AutoHeight = false;
            this.txtNGCount.Properties.ReadOnly = true;
            this.txtNGCount.Properties.UseReadOnlyAppearance = false;
            this.txtNGCount.Size = new System.Drawing.Size(612, 23);
            this.txtNGCount.TabIndex = 1;
            // 
            // txtNCRIssueDate
            // 
            this.txtNCRIssueDate.AutoHeight = false;
            this.txtNCRIssueDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNCRIssueDate.LanguageKey = "NCRISSUEDATE";
            this.txtNCRIssueDate.Location = new System.Drawing.Point(0, 0);
            this.txtNCRIssueDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtNCRIssueDate.Name = "txtNCRIssueDate";
            this.txtNCRIssueDate.Properties.AutoHeight = false;
            this.txtNCRIssueDate.Properties.ReadOnly = true;
            this.txtNCRIssueDate.Properties.UseReadOnlyAppearance = false;
            this.txtNCRIssueDate.Size = new System.Drawing.Size(612, 23);
            this.txtNCRIssueDate.TabIndex = 0;
            // 
            // tabNCRPic
            // 
            this.tabNCRPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNCRPic.Location = new System.Drawing.Point(0, 203);
            this.tabNCRPic.Margin = new System.Windows.Forms.Padding(0);
            this.tabNCRPic.Name = "tabNCRPic";
            this.tabNCRPic.SelectedTabPage = this.tpgNCR;
            this.tabNCRPic.Size = new System.Drawing.Size(1234, 595);
            this.tabNCRPic.TabIndex = 9;
            this.tabNCRPic.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgNCR,
            this.tpgPic});
            // 
            // tpgNCR
            // 
            this.tpgNCR.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabNCRPic.SetLanguageKey(this.tpgNCR, "NCRBTN");
            this.tpgNCR.Name = "tpgNCR";
            this.tpgNCR.Size = new System.Drawing.Size(1228, 566);
            this.tpgNCR.Text = "xtraTabPage1";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.issueRegistrationControl, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.ncrProgressControl, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1228, 566);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // issueRegistrationControl
            // 
            this.issueRegistrationControl.abnormalType = null;
            this.issueRegistrationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.issueRegistrationControl.isShowReasonCombo = false;
            this.issueRegistrationControl.Location = new System.Drawing.Point(0, 232);
            this.issueRegistrationControl.Margin = new System.Windows.Forms.Padding(0);
            this.issueRegistrationControl.Name = "issueRegistrationControl";
            this.issueRegistrationControl.ParentDataRow = null;
            this.issueRegistrationControl.ReasonTableVisible = false;
            this.issueRegistrationControl.Size = new System.Drawing.Size(1228, 334);
            this.issueRegistrationControl.TabIndex = 3;
            // 
            // ncrProgressControl
            // 
            this.ncrProgressControl.CurrentDataRow = null;
            this.ncrProgressControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ncrProgressControl.Location = new System.Drawing.Point(0, 0);
            this.ncrProgressControl.Margin = new System.Windows.Forms.Padding(0);
            this.ncrProgressControl.Name = "ncrProgressControl";
            this.ncrProgressControl.Size = new System.Drawing.Size(1228, 222);
            this.ncrProgressControl.TabIndex = 2;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 222);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(1228, 5);
            this.smartSpliterControl1.TabIndex = 0;
            this.smartSpliterControl1.TabStop = false;
            // 
            // tpgPic
            // 
            this.tpgPic.Controls.Add(this.tableLayoutPanel3);
            this.tabNCRPic.SetLanguageKey(this.tpgPic, "DEFECTPICTURE");
            this.tpgPic.Name = "tpgPic";
            this.tpgPic.Size = new System.Drawing.Size(774, 264);
            this.tpgPic.Text = "xtraTabPage2";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.flowMeasuredPicture, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(774, 264);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // flowMeasuredPicture
            // 
            this.flowMeasuredPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMeasuredPicture.Location = new System.Drawing.Point(0, 35);
            this.flowMeasuredPicture.Margin = new System.Windows.Forms.Padding(0);
            this.flowMeasuredPicture.Name = "flowMeasuredPicture";
            this.flowMeasuredPicture.Size = new System.Drawing.Size(774, 229);
            this.flowMeasuredPicture.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnDownload);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(774, 35);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btnDownload
            // 
            this.btnDownload.AllowFocus = false;
            this.btnDownload.IsBusy = false;
            this.btnDownload.IsWrite = false;
            this.btnDownload.LanguageKey = "FILEDOWNLOAD";
            this.btnDownload.Location = new System.Drawing.Point(691, 4);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDownload.Size = new System.Drawing.Size(80, 25);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "다운로드";
            this.btnDownload.TooltipLanguageKey = "";
            // 
            // ShipmentInspAbnormalOccurrencePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 853);
            this.LanguageKey = "SHIPMENTABNORMALOCCURRENCEPOPUP";
            this.Name = "ShipmentInspAbnormalOccurrencePopup";
            this.Text = "ShipmentInspAbnormalOccurrencePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).EndInit();
            this.grpInfo.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInspectionQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductdefName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductdefId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReasonCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNGCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNCRIssueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabNCRPic)).EndInit();
            this.tabNCRPic.ResumeLayout(false);
            this.tpgNCR.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.tpgPic.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox grpInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartLabelTextBox txtNCRIssueDate;
        private Framework.SmartControls.SmartLabelTextBox txtParentLotId;
        private Framework.SmartControls.SmartLabelTextBox txtDefectQty;
        private Framework.SmartControls.SmartLabelTextBox txtProductdefId;
        private Framework.SmartControls.SmartLabelTextBox txtDefectCodeName;
        private Framework.SmartControls.SmartLabelTextBox txtReasonCode;
        private Framework.SmartControls.SmartLabelTextBox txtNGCount;
        private Framework.SmartControls.SmartLabelTextBox txtDefectRate;
        private Framework.SmartControls.SmartLabelTextBox txtInspectionQty;
        private Framework.SmartControls.SmartLabelTextBox txtProductdefName;
        private Framework.SmartControls.SmartLabelTextBox txtDescription;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Framework.SmartControls.SmartTabControl tabNCRPic;
        private DevExpress.XtraTab.XtraTabPage tpgNCR;
        private DevExpress.XtraTab.XtraTabPage tpgPic;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private IssueRegistrationControl issueRegistrationControl;
        private NCRProgressControl ncrProgressControl;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowMeasuredPicture;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnDownload;
    }
}