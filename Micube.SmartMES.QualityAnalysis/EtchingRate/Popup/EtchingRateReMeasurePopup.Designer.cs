namespace Micube.SmartMES.QualityAnalysis
{
    partial class EtchingRateReMeasurePopup
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
            this.grpMeasure = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpValue = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtReValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblReValue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblRelAfter = new Micube.Framework.SmartControls.SmartLabel();
            this.numReAfter = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.numReBefore = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.lblReBefore = new Micube.Framework.SmartControls.SmartLabel();
            this.txtReResult = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtReMeasurer = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtReMeasureDegree = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdReSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnOK = new Micube.Framework.SmartControls.SmartButton();
            this.txtReMeasureDate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grpAction = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.memoActionResult = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOriginalFile = new Micube.Framework.SmartControls.SmartButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMeasure)).BeginInit();
            this.grpMeasure.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpValue)).BeginInit();
            this.grpValue.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReAfter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReBefore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReMeasurer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReMeasureDegree.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReMeasureDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAction)).BeginInit();
            this.grpAction.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoActionResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOriginalFile.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grpMeasure);
            this.pnlMain.Size = new System.Drawing.Size(1346, 315);
            // 
            // grpMeasure
            // 
            this.grpMeasure.Controls.Add(this.tableLayoutPanel1);
            this.grpMeasure.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMeasure.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpMeasure.LanguageKey = "ETCHINGRATESPECDEFINITION";
            this.grpMeasure.Location = new System.Drawing.Point(0, 0);
            this.grpMeasure.Margin = new System.Windows.Forms.Padding(0);
            this.grpMeasure.Name = "grpMeasure";
            this.grpMeasure.ShowBorder = true;
            this.grpMeasure.Size = new System.Drawing.Size(1346, 315);
            this.grpMeasure.TabIndex = 1;
            this.grpMeasure.Text = "고정항목";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.65086F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67457F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67457F));
            this.tableLayoutPanel1.Controls.Add(this.grpValue, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtReResult, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtReMeasurer, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtReMeasureDegree, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.grdReSpec, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 4, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtReMeasureDate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.grpAction, 4, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.02041F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.2449F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.2449F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.2449F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.2449F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1342, 282);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // grpValue
            // 
            this.grpValue.Controls.Add(this.tableLayoutPanel2);
            this.grpValue.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpValue.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpValue.LanguageKey = "WEIGHTVALUE";
            this.grpValue.Location = new System.Drawing.Point(441, 124);
            this.grpValue.Margin = new System.Windows.Forms.Padding(0);
            this.grpValue.Name = "grpValue";
            this.tableLayoutPanel1.SetRowSpan(this.grpValue, 7);
            this.grpValue.ShowBorder = true;
            this.grpValue.Size = new System.Drawing.Size(445, 127);
            this.grpValue.TabIndex = 7;
            this.grpValue.Text = "무게값";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.txtReValue, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblReValue, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblRelAfter, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.numReAfter, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.numReBefore, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblReBefore, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(441, 94);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtReValue
            // 
            this.txtReValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReValue.Enabled = false;
            this.txtReValue.LabelText = null;
            this.txtReValue.LanguageKey = null;
            this.txtReValue.Location = new System.Drawing.Point(225, 63);
            this.txtReValue.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.txtReValue.Name = "txtReValue";
            this.txtReValue.Properties.AutoHeight = false;
            this.txtReValue.Size = new System.Drawing.Size(211, 24);
            this.txtReValue.TabIndex = 12;
            // 
            // lblReValue
            // 
            this.lblReValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReValue.LanguageKey = "ETCHRATEVALUE";
            this.lblReValue.Location = new System.Drawing.Point(5, 63);
            this.lblReValue.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblReValue.Name = "lblReValue";
            this.lblReValue.Size = new System.Drawing.Size(210, 24);
            this.lblReValue.TabIndex = 11;
            this.lblReValue.Text = "에칭레이트 값";
            // 
            // lblRelAfter
            // 
            this.lblRelAfter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRelAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRelAfter.Location = new System.Drawing.Point(5, 34);
            this.lblRelAfter.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblRelAfter.Name = "lblRelAfter";
            this.lblRelAfter.Size = new System.Drawing.Size(210, 24);
            this.lblRelAfter.TabIndex = 10;
            this.lblRelAfter.Text = "에칭 후";
            // 
            // numReAfter
            // 
            this.numReAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numReAfter.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numReAfter.LabelText = null;
            this.numReAfter.LanguageKey = "ETCHRATEAFTERVALUE";
            this.numReAfter.Location = new System.Drawing.Point(225, 34);
            this.numReAfter.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.numReAfter.Name = "numReAfter";
            this.numReAfter.Properties.AutoHeight = false;
            this.numReAfter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numReAfter.Size = new System.Drawing.Size(211, 24);
            this.numReAfter.TabIndex = 9;
            // 
            // numReBefore
            // 
            this.numReBefore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numReBefore.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numReBefore.LabelText = null;
            this.numReBefore.LanguageKey = null;
            this.numReBefore.Location = new System.Drawing.Point(225, 5);
            this.numReBefore.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.numReBefore.Name = "numReBefore";
            this.numReBefore.Properties.AutoHeight = false;
            this.numReBefore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numReBefore.Size = new System.Drawing.Size(211, 24);
            this.numReBefore.TabIndex = 8;
            // 
            // lblReBefore
            // 
            this.lblReBefore.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReBefore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReBefore.LanguageKey = "ETCHRATEBEFOREVALUE";
            this.lblReBefore.Location = new System.Drawing.Point(5, 5);
            this.lblReBefore.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblReBefore.Name = "lblReBefore";
            this.lblReBefore.Size = new System.Drawing.Size(210, 24);
            this.lblReBefore.TabIndex = 4;
            this.lblReBefore.Text = "에칭 전";
            // 
            // txtReResult
            // 
            this.txtReResult.AutoHeight = false;
            this.txtReResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReResult.Enabled = false;
            this.txtReResult.LabelText = "판정 여부";
            this.txtReResult.LanguageKey = "RESULT";
            this.txtReResult.Location = new System.Drawing.Point(5, 223);
            this.txtReResult.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtReResult.Name = "txtReResult";
            this.txtReResult.Properties.AutoHeight = false;
            this.txtReResult.Size = new System.Drawing.Size(426, 28);
            this.txtReResult.TabIndex = 6;
            // 
            // txtReMeasurer
            // 
            this.txtReMeasurer.AutoHeight = false;
            this.txtReMeasurer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReMeasurer.LabelText = "측정자";
            this.txtReMeasurer.LanguageKey = "MEASURER";
            this.txtReMeasurer.Location = new System.Drawing.Point(5, 190);
            this.txtReMeasurer.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtReMeasurer.Name = "txtReMeasurer";
            this.txtReMeasurer.Properties.AutoHeight = false;
            this.txtReMeasurer.Size = new System.Drawing.Size(426, 28);
            this.txtReMeasurer.TabIndex = 5;
            // 
            // txtReMeasureDegree
            // 
            this.txtReMeasureDegree.AutoHeight = false;
            this.txtReMeasureDegree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReMeasureDegree.Enabled = false;
            this.txtReMeasureDegree.LabelText = "재측정 회차";
            this.txtReMeasureDegree.LanguageKey = "REMEASUREDEGREE";
            this.txtReMeasureDegree.Location = new System.Drawing.Point(5, 157);
            this.txtReMeasureDegree.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtReMeasureDegree.Name = "txtReMeasureDegree";
            this.txtReMeasureDegree.Properties.AutoHeight = false;
            this.txtReMeasureDegree.Size = new System.Drawing.Size(426, 28);
            this.txtReMeasureDegree.TabIndex = 4;
            // 
            // grdReSpec
            // 
            this.grdReSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel1.SetColumnSpan(this.grdReSpec, 5);
            this.grdReSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReSpec.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReSpec.IsUsePaging = false;
            this.grdReSpec.LanguageKey = null;
            this.grdReSpec.Location = new System.Drawing.Point(0, 0);
            this.grdReSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdReSpec.Name = "grdReSpec";
            this.grdReSpec.ShowBorder = false;
            this.grdReSpec.ShowButtonBar = false;
            this.grdReSpec.ShowStatusBar = false;
            this.grdReSpec.Size = new System.Drawing.Size(1342, 119);
            this.grdReSpec.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(896, 256);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(446, 26);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(371, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnOK
            // 
            this.btnOK.AllowFocus = false;
            this.btnOK.IsBusy = false;
            this.btnOK.IsWrite = false;
            this.btnOK.LanguageKey = "OK";
            this.btnOK.Location = new System.Drawing.Point(290, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "확인";
            this.btnOK.TooltipLanguageKey = "";
            // 
            // txtReMeasureDate
            // 
            this.txtReMeasureDate.AutoHeight = false;
            this.txtReMeasureDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReMeasureDate.Enabled = false;
            this.txtReMeasureDate.LabelText = "재측정 일자";
            this.txtReMeasureDate.LanguageKey = "REMEASUREDATE";
            this.txtReMeasureDate.Location = new System.Drawing.Point(5, 124);
            this.txtReMeasureDate.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtReMeasureDate.Name = "txtReMeasureDate";
            this.txtReMeasureDate.Properties.AutoHeight = false;
            this.txtReMeasureDate.Size = new System.Drawing.Size(426, 28);
            this.txtReMeasureDate.TabIndex = 3;
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.tableLayoutPanel3);
            this.grpAction.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAction.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpAction.LanguageKey = "ACTIONRESULT";
            this.grpAction.Location = new System.Drawing.Point(896, 124);
            this.grpAction.Margin = new System.Windows.Forms.Padding(0);
            this.grpAction.Name = "grpAction";
            this.tableLayoutPanel1.SetRowSpan(this.grpAction, 7);
            this.grpAction.ShowBorder = true;
            this.grpAction.Size = new System.Drawing.Size(446, 127);
            this.grpAction.TabIndex = 8;
            this.grpAction.Text = "smartGroupBox1";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.Controls.Add(this.btnOriginalFile, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.memoActionResult, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 2, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(442, 94);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // memoActionResult
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.memoActionResult, 2);
            this.memoActionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoActionResult.Location = new System.Drawing.Point(5, 37);
            this.memoActionResult.Margin = new System.Windows.Forms.Padding(0);
            this.memoActionResult.Name = "memoActionResult";
            this.memoActionResult.Size = new System.Drawing.Size(432, 51);
            this.memoActionResult.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(437, 5);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(5, 27);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // btnOriginalFile
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.btnOriginalFile, 2);
            this.btnOriginalFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOriginalFile.Location = new System.Drawing.Point(5, 5);
            this.btnOriginalFile.Margin = new System.Windows.Forms.Padding(0);
            this.btnOriginalFile.Name = "btnOriginalFile";
            this.btnOriginalFile.Properties.AutoHeight = false;
            this.btnOriginalFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnOriginalFile.Size = new System.Drawing.Size(432, 27);
            this.btnOriginalFile.TabIndex = 25;
            // 
            // EtchingRateReMeasurePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 335);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "ETCHINGRATEREMEASUREPOPUP";
            this.Name = "EtchingRateReMeasurePopup";
            this.Text = "EtchingRateReMeasurePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpMeasure)).EndInit();
            this.grpMeasure.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpValue)).EndInit();
            this.grpValue.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReAfter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReBefore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReMeasurer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReMeasureDegree.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReMeasureDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAction)).EndInit();
            this.grpAction.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoActionResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOriginalFile.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartGroupBox grpMeasure;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdReSpec;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnOK;
        private Framework.SmartControls.SmartLabelTextBox txtReResult;
        private Framework.SmartControls.SmartLabelTextBox txtReMeasurer;
        private Framework.SmartControls.SmartLabelTextBox txtReMeasureDegree;
        private Framework.SmartControls.SmartLabelTextBox txtReMeasureDate;
        private Framework.SmartControls.SmartGroupBox grpValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartLabel lblRelAfter;
        private Framework.SmartControls.SmartSpinEdit numReAfter;
        private Framework.SmartControls.SmartLabel lblReBefore;
        private Framework.SmartControls.SmartSpinEdit numReBefore;
        private Framework.SmartControls.SmartLabel lblReValue;
        private Framework.SmartControls.SmartTextBox txtReValue;
        private Framework.SmartControls.SmartGroupBox grpAction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartMemoEdit memoActionResult;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButtonEdit btnOriginalFile;
    }
}