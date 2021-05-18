namespace Micube.SmartMES.ProcessManagement
{
    partial class SelectResourcePopup
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
            this.tlpResource = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.pnlTitle = new Micube.Framework.SmartControls.SmartPanel();
            this.lblTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.cboResource = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.pnlButton = new Micube.Framework.SmartControls.SmartPanel();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancle = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tlpResource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitle)).BeginInit();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tlpResource);
            this.pnlMain.Size = new System.Drawing.Size(480, 154);
            // 
            // tlpResource
            // 
            this.tlpResource.ColumnCount = 1;
            this.tlpResource.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpResource.Controls.Add(this.pnlTitle, 0, 0);
            this.tlpResource.Controls.Add(this.cboResource, 0, 2);
            this.tlpResource.Controls.Add(this.pnlButton, 0, 4);
            this.tlpResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpResource.Location = new System.Drawing.Point(0, 0);
            this.tlpResource.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpResource.Name = "tlpResource";
            this.tlpResource.RowCount = 5;
            this.tlpResource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpResource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpResource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpResource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpResource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpResource.Size = new System.Drawing.Size(480, 154);
            this.tlpResource.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(480, 84);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 84);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "현재 공정에서 사용되는 자원 정보가 없습니다. 현재 공정에서 사용할 자원을 선택하시기 바랍니다.";
            // 
            // cboResource
            // 
            this.cboResource.Appearance.ForeColor = System.Drawing.Color.Red;
            this.cboResource.Appearance.Options.UseForeColor = true;
            this.cboResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboResource.EditorWidth = "80%";
            this.cboResource.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cboResource.LabelText = "자원";
            this.cboResource.LabelWidth = "20%";
            this.cboResource.LanguageKey = "RESOURCE";
            this.cboResource.Location = new System.Drawing.Point(0, 94);
            this.cboResource.Margin = new System.Windows.Forms.Padding(0);
            this.cboResource.Name = "cboResource";
            this.cboResource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboResource.Properties.NullText = "";
            this.cboResource.Size = new System.Drawing.Size(480, 20);
            this.cboResource.TabIndex = 1;
            // 
            // pnlButton
            // 
            this.pnlButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlButton.Controls.Add(this.btnCancle);
            this.pnlButton.Controls.Add(this.btnConfirm);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 124);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(480, 30);
            this.pnlButton.TabIndex = 2;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.IsWrite = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(405, 10);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConfirm.Size = new System.Drawing.Size(75, 20);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "확인";
            this.btnConfirm.TooltipLanguageKey = "";
            // 
            // btnCancle
            // 
            this.btnCancle.AllowFocus = false;
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.IsBusy = false;
            this.btnCancle.IsWrite = false;
            this.btnCancle.LanguageKey = "CANCEL";
            this.btnCancle.Location = new System.Drawing.Point(315, 10);
            this.btnCancle.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancle.Size = new System.Drawing.Size(75, 20);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "취소";
            this.btnCancle.TooltipLanguageKey = "";
            // 
            // SelectResourcePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 174);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LanguageKey = "SELECTRESOURCEPOPUP";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectResourcePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SelectResourcePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tlpResource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTitle)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpResource;
        private Framework.SmartControls.SmartPanel pnlTitle;
        private Framework.SmartControls.SmartLabel lblTitle;
        private Framework.SmartControls.SmartLabelComboBox cboResource;
        private Framework.SmartControls.SmartPanel pnlButton;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnCancle;
    }
}