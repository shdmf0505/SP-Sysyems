namespace SmartMES
{
    partial class ForgotPassword
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
            this.panUserId = new DevExpress.XtraEditors.PanelControl();
            this.txtUserId = new DevExpress.XtraEditors.TextEdit();
            this.lblUserId = new DevExpress.XtraEditors.LabelControl();
            this.panMain = new DevExpress.XtraEditors.PanelControl();
            this.panButton = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panUserId)).BeginInit();
            this.panUserId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panUserId
            // 
            this.panUserId.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panUserId.Controls.Add(this.txtUserId);
            this.panUserId.Controls.Add(this.lblUserId);
            this.panUserId.Dock = System.Windows.Forms.DockStyle.Top;
            this.panUserId.Location = new System.Drawing.Point(10, 10);
            this.panUserId.Margin = new System.Windows.Forms.Padding(0);
            this.panUserId.Name = "panUserId";
            this.panUserId.Size = new System.Drawing.Size(262, 26);
            this.panUserId.TabIndex = 0;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(106, 2);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtUserId.Properties.Appearance.Options.UseFont = true;
            this.txtUserId.Properties.AutoHeight = false;
            this.txtUserId.Size = new System.Drawing.Size(150, 22);
            this.txtUserId.TabIndex = 1;
            // 
            // lblUserId
            // 
            this.lblUserId.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblUserId.Appearance.Options.UseFont = true;
            this.lblUserId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblUserId.Location = new System.Drawing.Point(5, 2);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(100, 22);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "사용자 ID";
            // 
            // panMain
            // 
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.Controls.Add(this.panButton);
            this.panMain.Controls.Add(this.panUserId);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Margin = new System.Windows.Forms.Padding(0);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(10);
            this.panMain.Size = new System.Drawing.Size(282, 79);
            this.panMain.TabIndex = 1;
            // 
            // panButton
            // 
            this.panButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panButton.Controls.Add(this.btnCancel);
            this.panButton.Controls.Add(this.btnConfirm);
            this.panButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panButton.Location = new System.Drawing.Point(10, 36);
            this.panButton.Margin = new System.Windows.Forms.Padding(0);
            this.panButton.Name = "panButton";
            this.panButton.Size = new System.Drawing.Size(262, 33);
            this.panButton.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(181, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "취소";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.Location = new System.Drawing.Point(96, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "확인";
            // 
            // ForgotPassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(282, 79);
            this.Controls.Add(this.panMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForgotPassword";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "비밀번호 초기화";
            ((System.ComponentModel.ISupportInitialize)(this.panUserId)).EndInit();
            this.panUserId.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panUserId;
        private DevExpress.XtraEditors.LabelControl lblUserId;
        private DevExpress.XtraEditors.TextEdit txtUserId;
        private DevExpress.XtraEditors.PanelControl panMain;
        private DevExpress.XtraEditors.PanelControl panButton;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}