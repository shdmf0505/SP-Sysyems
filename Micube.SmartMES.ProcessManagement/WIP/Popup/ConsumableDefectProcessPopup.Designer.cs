namespace Micube.SmartMES.ProcessManagement
{
    partial class ConsumableDefectProcessPopup
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblReasonCode = new Micube.Framework.SmartControls.SmartLabel();
            this.lblDefectQty = new Micube.Framework.SmartControls.SmartLabel();
            this.lblConsumableLotId = new Micube.Framework.SmartControls.SmartLabel();
            this.txtConsumableLotId = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblCurrentQty = new Micube.Framework.SmartControls.SmartLabel();
            this.numCurrentQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.numDefectQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.cboReasonCode = new Micube.Framework.SmartControls.SmartComboBox();
            this.pnlButton = new Micube.Framework.SmartControls.SmartPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumableLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDefectQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(380, 120);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 4;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblReasonCode, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblDefectQty, 2, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblConsumableLotId, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtConsumableLotId, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblCurrentQty, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.numCurrentQty, 1, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.numDefectQty, 3, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboReasonCode, 1, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlButton, 2, 6);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 7;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(380, 120);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // lblReasonCode
            // 
            this.lblReasonCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReasonCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReasonCode.LanguageKey = "REASON";
            this.lblReasonCode.Location = new System.Drawing.Point(0, 60);
            this.lblReasonCode.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblReasonCode.Name = "lblReasonCode";
            this.lblReasonCode.Size = new System.Drawing.Size(90, 20);
            this.lblReasonCode.TabIndex = 6;
            this.lblReasonCode.Text = "사유";
            // 
            // lblDefectQty
            // 
            this.lblDefectQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDefectQty.LanguageKey = "DEFECTQTY";
            this.lblDefectQty.Location = new System.Drawing.Point(190, 30);
            this.lblDefectQty.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblDefectQty.Name = "lblDefectQty";
            this.lblDefectQty.Size = new System.Drawing.Size(90, 20);
            this.lblDefectQty.TabIndex = 4;
            this.lblDefectQty.Text = "현재 수량";
            // 
            // lblConsumableLotId
            // 
            this.lblConsumableLotId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblConsumableLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConsumableLotId.LanguageKey = "MATERIALLOT";
            this.lblConsumableLotId.Location = new System.Drawing.Point(0, 0);
            this.lblConsumableLotId.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblConsumableLotId.Name = "lblConsumableLotId";
            this.lblConsumableLotId.Size = new System.Drawing.Size(90, 20);
            this.lblConsumableLotId.TabIndex = 0;
            this.lblConsumableLotId.Text = "자재 LOT";
            // 
            // txtConsumableLotId
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.txtConsumableLotId, 3);
            this.txtConsumableLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsumableLotId.LabelText = null;
            this.txtConsumableLotId.LanguageKey = null;
            this.txtConsumableLotId.Location = new System.Drawing.Point(100, 0);
            this.txtConsumableLotId.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.txtConsumableLotId.Name = "txtConsumableLotId";
            this.txtConsumableLotId.Properties.AutoHeight = false;
            this.txtConsumableLotId.Properties.ReadOnly = true;
            this.txtConsumableLotId.Size = new System.Drawing.Size(270, 20);
            this.txtConsumableLotId.TabIndex = 1;
            // 
            // lblCurrentQty
            // 
            this.lblCurrentQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCurrentQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentQty.LanguageKey = "AVAILABLEQTY";
            this.lblCurrentQty.Location = new System.Drawing.Point(0, 30);
            this.lblCurrentQty.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblCurrentQty.Name = "lblCurrentQty";
            this.lblCurrentQty.Size = new System.Drawing.Size(90, 20);
            this.lblCurrentQty.TabIndex = 2;
            this.lblCurrentQty.Text = "가용 수량";
            // 
            // numCurrentQty
            // 
            this.numCurrentQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numCurrentQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numCurrentQty.LabelText = null;
            this.numCurrentQty.LanguageKey = null;
            this.numCurrentQty.Location = new System.Drawing.Point(100, 30);
            this.numCurrentQty.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.numCurrentQty.Name = "numCurrentQty";
            this.numCurrentQty.Properties.AutoHeight = false;
            this.numCurrentQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numCurrentQty.Properties.Mask.EditMask = "#,##0.#####";
            this.numCurrentQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numCurrentQty.Properties.ReadOnly = true;
            this.numCurrentQty.Size = new System.Drawing.Size(80, 20);
            this.numCurrentQty.TabIndex = 3;
            // 
            // numDefectQty
            // 
            this.numDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numDefectQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDefectQty.LabelText = null;
            this.numDefectQty.LanguageKey = null;
            this.numDefectQty.Location = new System.Drawing.Point(290, 30);
            this.numDefectQty.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.numDefectQty.Name = "numDefectQty";
            this.numDefectQty.Properties.AutoHeight = false;
            this.numDefectQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numDefectQty.Properties.DisplayFormat.FormatString = "#,##0";
            this.numDefectQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numDefectQty.Properties.Mask.EditMask = "#,##0.#####";
            this.numDefectQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numDefectQty.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numDefectQty.Size = new System.Drawing.Size(80, 20);
            this.numDefectQty.TabIndex = 5;
            // 
            // cboReasonCode
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.cboReasonCode, 2);
            this.cboReasonCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReasonCode.LabelText = null;
            this.cboReasonCode.LanguageKey = null;
            this.cboReasonCode.Location = new System.Drawing.Point(100, 60);
            this.cboReasonCode.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cboReasonCode.Name = "cboReasonCode";
            this.cboReasonCode.PopupWidth = 0;
            this.cboReasonCode.Properties.AutoHeight = false;
            this.cboReasonCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReasonCode.Properties.NullText = "";
            this.cboReasonCode.ShowHeader = true;
            this.cboReasonCode.Size = new System.Drawing.Size(180, 20);
            this.cboReasonCode.TabIndex = 7;
            this.cboReasonCode.VisibleColumns = null;
            this.cboReasonCode.VisibleColumnsWidth = null;
            // 
            // pnlButton
            // 
            this.pnlButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.pnlButton, 2);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnSave);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(190, 100);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(190, 20);
            this.pnlButton.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(115, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 20);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "취소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(30, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 20);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // ConsumableDefectProcessPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 140);
            this.LanguageKey = "CONSUMABLEDEFECTPROCESS";
            this.Name = "ConsumableDefectProcessPopup";
            this.Text = "ConsumableDefectProcessPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumableLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDefectQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartLabel lblConsumableLotId;
        private Framework.SmartControls.SmartTextBox txtConsumableLotId;
        private Framework.SmartControls.SmartLabel lblCurrentQty;
        private Framework.SmartControls.SmartSpinEdit numCurrentQty;
        private Framework.SmartControls.SmartLabel lblDefectQty;
        private Framework.SmartControls.SmartSpinEdit numDefectQty;
        private Framework.SmartControls.SmartLabel lblReasonCode;
        private Framework.SmartControls.SmartComboBox cboReasonCode;
        private Framework.SmartControls.SmartPanel pnlButton;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnSave;
    }
}