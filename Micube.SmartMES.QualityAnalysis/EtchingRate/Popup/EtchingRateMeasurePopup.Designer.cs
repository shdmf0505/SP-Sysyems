namespace Micube.SmartMES.QualityAnalysis
{
    partial class EtchingRateMeasurePopup
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
            this.txtArea = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblArea = new Micube.Framework.SmartControls.SmartLabel();
            this.txtCycle = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblMeasurer = new Micube.Framework.SmartControls.SmartLabel();
            this.lblEqpVelocity = new Micube.Framework.SmartControls.SmartLabel();
            this.grdSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grpValue = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAfter = new Micube.Framework.SmartControls.SmartLabel();
            this.numAfter = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.numBefore = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.lblValue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblBefore = new Micube.Framework.SmartControls.SmartLabel();
            this.txtValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnOK = new Micube.Framework.SmartControls.SmartButton();
            this.lblCycle = new Micube.Framework.SmartControls.SmartLabel();
            this.numEqpVelocity = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.txtMeasurer = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMeasure)).BeginInit();
            this.grpMeasure.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpValue)).BeginInit();
            this.grpValue.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAfter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBefore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEqpVelocity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeasurer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grpMeasure);
            this.pnlMain.Size = new System.Drawing.Size(776, 244);
            // 
            // grpMeasure
            // 
            this.grpMeasure.Controls.Add(this.tableLayoutPanel1);
            this.grpMeasure.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMeasure.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpMeasure.Location = new System.Drawing.Point(0, 0);
            this.grpMeasure.Margin = new System.Windows.Forms.Padding(0);
            this.grpMeasure.Name = "grpMeasure";
            this.grpMeasure.ShowBorder = true;
            this.grpMeasure.Size = new System.Drawing.Size(776, 244);
            this.grpMeasure.TabIndex = 0;
            this.grpMeasure.Text = "고정항목";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.txtArea, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblArea, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtCycle, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblMeasurer, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblEqpVelocity, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.grdSpec, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpValue, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblCycle, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numEqpVelocity, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtMeasurer, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.02041F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 211);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // txtArea
            // 
            this.txtArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArea.LabelText = null;
            this.txtArea.LanguageKey = null;
            this.txtArea.Location = new System.Drawing.Point(152, 182);
            this.txtArea.Margin = new System.Windows.Forms.Padding(0);
            this.txtArea.Name = "txtArea";
            this.txtArea.Properties.AutoHeight = false;
            this.txtArea.Size = new System.Drawing.Size(152, 29);
            this.txtArea.TabIndex = 11;
            // 
            // lblArea
            // 
            this.lblArea.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblArea.LanguageKey = "AREA";
            this.lblArea.Location = new System.Drawing.Point(5, 182);
            this.lblArea.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(147, 29);
            this.lblArea.TabIndex = 10;
            this.lblArea.Text = "작업장";
            // 
            // txtCycle
            // 
            this.txtCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCycle.Enabled = false;
            this.txtCycle.LabelText = null;
            this.txtCycle.LanguageKey = null;
            this.txtCycle.Location = new System.Drawing.Point(152, 89);
            this.txtCycle.Margin = new System.Windows.Forms.Padding(0);
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.Properties.AutoHeight = false;
            this.txtCycle.Size = new System.Drawing.Size(152, 26);
            this.txtCycle.TabIndex = 9;
            // 
            // lblMeasurer
            // 
            this.lblMeasurer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMeasurer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMeasurer.LanguageKey = "MEASURER";
            this.lblMeasurer.Location = new System.Drawing.Point(5, 151);
            this.lblMeasurer.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblMeasurer.Name = "lblMeasurer";
            this.lblMeasurer.Size = new System.Drawing.Size(147, 26);
            this.lblMeasurer.TabIndex = 5;
            this.lblMeasurer.Text = "측정자";
            // 
            // lblEqpVelocity
            // 
            this.lblEqpVelocity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblEqpVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEqpVelocity.LanguageKey = "EQPTSPEED";
            this.lblEqpVelocity.Location = new System.Drawing.Point(5, 120);
            this.lblEqpVelocity.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblEqpVelocity.Name = "lblEqpVelocity";
            this.lblEqpVelocity.Size = new System.Drawing.Size(147, 26);
            this.lblEqpVelocity.TabIndex = 4;
            this.lblEqpVelocity.Text = "설비속도";
            // 
            // grdSpec
            // 
            this.grdSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel1.SetColumnSpan(this.grdSpec, 4);
            this.grdSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSpec.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSpec.IsUsePaging = false;
            this.grdSpec.LanguageKey = "ETCHINGRATESPECDEFINITION";
            this.grdSpec.Location = new System.Drawing.Point(0, 0);
            this.grdSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdSpec.Name = "grdSpec";
            this.grdSpec.ShowBorder = false;
            this.grdSpec.ShowButtonBar = false;
            this.grdSpec.ShowStatusBar = false;
            this.grdSpec.Size = new System.Drawing.Size(772, 84);
            this.grdSpec.TabIndex = 0;
            // 
            // grpValue
            // 
            this.grpValue.Controls.Add(this.tableLayoutPanel2);
            this.grpValue.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpValue.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpValue.LanguageKey = "WEIGHTVALUE";
            this.grpValue.Location = new System.Drawing.Point(314, 89);
            this.grpValue.Margin = new System.Windows.Forms.Padding(0);
            this.grpValue.Name = "grpValue";
            this.tableLayoutPanel1.SetRowSpan(this.grpValue, 5);
            this.grpValue.ShowBorder = true;
            this.grpValue.Size = new System.Drawing.Size(458, 88);
            this.grpValue.TabIndex = 1;
            this.grpValue.Text = "무게값";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.lblAfter, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.numAfter, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.numBefore, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblValue, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblBefore, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtValue, 3, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(454, 55);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAfter.LanguageKey = "ETCHRATEAFTERVALUE";
            this.lblAfter.Location = new System.Drawing.Point(5, 30);
            this.lblAfter.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(142, 20);
            this.lblAfter.TabIndex = 10;
            this.lblAfter.Text = "에칭 후";
            // 
            // numAfter
            // 
            this.numAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numAfter.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numAfter.LabelText = null;
            this.numAfter.LanguageKey = null;
            this.numAfter.Location = new System.Drawing.Point(147, 30);
            this.numAfter.Margin = new System.Windows.Forms.Padding(0);
            this.numAfter.Name = "numAfter";
            this.numAfter.Properties.AutoHeight = false;
            this.numAfter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numAfter.Size = new System.Drawing.Size(148, 20);
            this.numAfter.TabIndex = 9;
            // 
            // numBefore
            // 
            this.numBefore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numBefore.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numBefore.LabelText = null;
            this.numBefore.LanguageKey = null;
            this.numBefore.Location = new System.Drawing.Point(147, 5);
            this.numBefore.Margin = new System.Windows.Forms.Padding(0);
            this.numBefore.Name = "numBefore";
            this.numBefore.Properties.AutoHeight = false;
            this.numBefore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numBefore.Size = new System.Drawing.Size(148, 20);
            this.numBefore.TabIndex = 8;
            // 
            // lblValue
            // 
            this.lblValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValue.LanguageKey = "ETCHRATEVALUE";
            this.lblValue.Location = new System.Drawing.Point(305, 5);
            this.lblValue.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(144, 20);
            this.lblValue.TabIndex = 5;
            this.lblValue.Text = "에칭레이트 값";
            // 
            // lblBefore
            // 
            this.lblBefore.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblBefore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBefore.LanguageKey = "ETCHRATEBEFOREVALUE";
            this.lblBefore.Location = new System.Drawing.Point(5, 5);
            this.lblBefore.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblBefore.Name = "lblBefore";
            this.lblBefore.Size = new System.Drawing.Size(142, 20);
            this.lblBefore.TabIndex = 4;
            this.lblBefore.Text = "에칭 전";
            // 
            // txtValue
            // 
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Enabled = false;
            this.txtValue.LabelText = null;
            this.txtValue.LanguageKey = null;
            this.txtValue.Location = new System.Drawing.Point(305, 30);
            this.txtValue.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.txtValue.Name = "txtValue";
            this.txtValue.Properties.AutoHeight = false;
            this.txtValue.Size = new System.Drawing.Size(144, 20);
            this.txtValue.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(314, 182);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(458, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(383, 0);
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
            this.btnOK.Location = new System.Drawing.Point(302, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "확인";
            this.btnOK.TooltipLanguageKey = "";
            // 
            // lblCycle
            // 
            this.lblCycle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCycle.LanguageKey = "MEASUREDEGREE";
            this.lblCycle.Location = new System.Drawing.Point(5, 89);
            this.lblCycle.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblCycle.Name = "lblCycle";
            this.lblCycle.Size = new System.Drawing.Size(147, 26);
            this.lblCycle.TabIndex = 3;
            this.lblCycle.Text = "에칭회차";
            // 
            // numEqpVelocity
            // 
            this.numEqpVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numEqpVelocity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numEqpVelocity.LabelText = null;
            this.numEqpVelocity.LanguageKey = null;
            this.numEqpVelocity.Location = new System.Drawing.Point(152, 120);
            this.numEqpVelocity.Margin = new System.Windows.Forms.Padding(0);
            this.numEqpVelocity.Name = "numEqpVelocity";
            this.numEqpVelocity.Properties.AutoHeight = false;
            this.numEqpVelocity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numEqpVelocity.Size = new System.Drawing.Size(152, 26);
            this.numEqpVelocity.TabIndex = 7;
            // 
            // txtMeasurer
            // 
            this.txtMeasurer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMeasurer.LabelText = null;
            this.txtMeasurer.LanguageKey = null;
            this.txtMeasurer.Location = new System.Drawing.Point(152, 151);
            this.txtMeasurer.Margin = new System.Windows.Forms.Padding(0);
            this.txtMeasurer.Name = "txtMeasurer";
            this.txtMeasurer.Properties.AutoHeight = false;
            this.txtMeasurer.Size = new System.Drawing.Size(152, 26);
            this.txtMeasurer.TabIndex = 8;
            // 
            // EtchingRateMeasurePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 264);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "ETCHINGRATEMEASUREPOPUP";
            this.Name = "EtchingRateMeasurePopup";
            this.Text = "EtchingRateMeasurePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpMeasure)).EndInit();
            this.grpMeasure.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpValue)).EndInit();
            this.grpValue.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAfter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBefore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numEqpVelocity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeasurer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartGroupBox grpMeasure;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdSpec;
        private Framework.SmartControls.SmartGroupBox grpValue;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnOK;
        private Framework.SmartControls.SmartLabel lblMeasurer;
        private Framework.SmartControls.SmartLabel lblEqpVelocity;
        private Framework.SmartControls.SmartLabel lblCycle;
        private Framework.SmartControls.SmartSpinEdit numEqpVelocity;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartSpinEdit numAfter;
        private Framework.SmartControls.SmartSpinEdit numBefore;
        private Framework.SmartControls.SmartLabel lblValue;
        private Framework.SmartControls.SmartLabel lblBefore;
        private Framework.SmartControls.SmartTextBox txtMeasurer;
        private Framework.SmartControls.SmartLabel lblAfter;
        private Framework.SmartControls.SmartTextBox txtValue;
        private Framework.SmartControls.SmartTextBox txtCycle;
        private Framework.SmartControls.SmartTextBox txtArea;
        private Framework.SmartControls.SmartLabel lblArea;
    }
}