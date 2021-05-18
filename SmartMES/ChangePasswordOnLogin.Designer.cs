namespace SmartMES
{
    partial class ChangePasswordOnLogin
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
            this.panCurrentPassword = new DevExpress.XtraEditors.PanelControl();
            this.txtCurrentPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblCurrentPassword = new DevExpress.XtraEditors.LabelControl();
            this.panMain = new DevExpress.XtraEditors.PanelControl();
            this.panButton = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.panNewPasswordConfirm = new DevExpress.XtraEditors.PanelControl();
            this.txtNewPasswordConfirm = new DevExpress.XtraEditors.TextEdit();
            this.lblNewPasswordConfirm = new DevExpress.XtraEditors.LabelControl();
            this.panNewPassword = new DevExpress.XtraEditors.PanelControl();
            this.txtNewPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblNewPassword = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panCurrentPassword)).BeginInit();
            this.panCurrentPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panNewPasswordConfirm)).BeginInit();
            this.panNewPasswordConfirm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPasswordConfirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panNewPassword)).BeginInit();
            this.panNewPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panCurrentPassword
            // 
            this.panCurrentPassword.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panCurrentPassword.Controls.Add(this.txtCurrentPassword);
            this.panCurrentPassword.Controls.Add(this.lblCurrentPassword);
            this.panCurrentPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.panCurrentPassword.Location = new System.Drawing.Point(10, 10);
            this.panCurrentPassword.Margin = new System.Windows.Forms.Padding(0);
            this.panCurrentPassword.Name = "panCurrentPassword";
            this.panCurrentPassword.Size = new System.Drawing.Size(280, 26);
            this.panCurrentPassword.TabIndex = 0;
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Location = new System.Drawing.Point(126, 2);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCurrentPassword.Properties.Appearance.Options.UseFont = true;
            this.txtCurrentPassword.Properties.AutoHeight = false;
            this.txtCurrentPassword.Properties.PasswordChar = '●';
            this.txtCurrentPassword.Size = new System.Drawing.Size(150, 22);
            this.txtCurrentPassword.TabIndex = 1;
            // 
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblCurrentPassword.Appearance.Options.UseFont = true;
            this.lblCurrentPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCurrentPassword.Location = new System.Drawing.Point(5, 2);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(120, 22);
            this.lblCurrentPassword.TabIndex = 0;
            this.lblCurrentPassword.Text = "현재 비밀번호";
            // 
            // panMain
            // 
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.Controls.Add(this.panButton);
            this.panMain.Controls.Add(this.panNewPasswordConfirm);
            this.panMain.Controls.Add(this.panNewPassword);
            this.panMain.Controls.Add(this.panCurrentPassword);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Margin = new System.Windows.Forms.Padding(0);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(10);
            this.panMain.Size = new System.Drawing.Size(300, 131);
            this.panMain.TabIndex = 1;
            // 
            // panButton
            // 
            this.panButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panButton.Controls.Add(this.btnConfirm);
            this.panButton.Controls.Add(this.btnCancel);
            this.panButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panButton.Location = new System.Drawing.Point(10, 88);
            this.panButton.Margin = new System.Windows.Forms.Padding(0);
            this.panButton.Name = "panButton";
            this.panButton.Size = new System.Drawing.Size(280, 33);
            this.panButton.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(201, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "취소";
            this.btnCancel.Visible = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.Location = new System.Drawing.Point(201, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "확인";
            // 
            // panNewPasswordConfirm
            // 
            this.panNewPasswordConfirm.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panNewPasswordConfirm.Controls.Add(this.txtNewPasswordConfirm);
            this.panNewPasswordConfirm.Controls.Add(this.lblNewPasswordConfirm);
            this.panNewPasswordConfirm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panNewPasswordConfirm.Location = new System.Drawing.Point(10, 62);
            this.panNewPasswordConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.panNewPasswordConfirm.Name = "panNewPasswordConfirm";
            this.panNewPasswordConfirm.Size = new System.Drawing.Size(280, 26);
            this.panNewPasswordConfirm.TabIndex = 3;
            // 
            // txtNewPasswordConfirm
            // 
            this.txtNewPasswordConfirm.Location = new System.Drawing.Point(126, 2);
            this.txtNewPasswordConfirm.Name = "txtNewPasswordConfirm";
            this.txtNewPasswordConfirm.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNewPasswordConfirm.Properties.Appearance.Options.UseFont = true;
            this.txtNewPasswordConfirm.Properties.AutoHeight = false;
            this.txtNewPasswordConfirm.Properties.PasswordChar = '●';
            this.txtNewPasswordConfirm.Size = new System.Drawing.Size(150, 22);
            this.txtNewPasswordConfirm.TabIndex = 3;
            // 
            // lblNewPasswordConfirm
            // 
            this.lblNewPasswordConfirm.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNewPasswordConfirm.Appearance.Options.UseFont = true;
            this.lblNewPasswordConfirm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNewPasswordConfirm.Location = new System.Drawing.Point(5, 2);
            this.lblNewPasswordConfirm.Name = "lblNewPasswordConfirm";
            this.lblNewPasswordConfirm.Size = new System.Drawing.Size(120, 22);
            this.lblNewPasswordConfirm.TabIndex = 2;
            this.lblNewPasswordConfirm.Text = "새 비밀번호 확인";
            // 
            // panNewPassword
            // 
            this.panNewPassword.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panNewPassword.Controls.Add(this.txtNewPassword);
            this.panNewPassword.Controls.Add(this.lblNewPassword);
            this.panNewPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.panNewPassword.Location = new System.Drawing.Point(10, 36);
            this.panNewPassword.Margin = new System.Windows.Forms.Padding(0);
            this.panNewPassword.Name = "panNewPassword";
            this.panNewPassword.Size = new System.Drawing.Size(280, 26);
            this.panNewPassword.TabIndex = 2;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(126, 2);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNewPassword.Properties.Appearance.Options.UseFont = true;
            this.txtNewPassword.Properties.AutoHeight = false;
            this.txtNewPassword.Properties.PasswordChar = '●';
            this.txtNewPassword.Size = new System.Drawing.Size(150, 22);
            this.txtNewPassword.TabIndex = 3;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNewPassword.Appearance.Options.UseFont = true;
            this.lblNewPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNewPassword.Location = new System.Drawing.Point(5, 2);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(120, 22);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "새 비밀번호";
            // 
            // ChangePasswordOnLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(300, 131);
            this.ControlBox = false;
            this.Controls.Add(this.panMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordOnLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "비밀번호 변경";
            ((System.ComponentModel.ISupportInitialize)(this.panCurrentPassword)).EndInit();
            this.panCurrentPassword.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panNewPasswordConfirm)).EndInit();
            this.panNewPasswordConfirm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPasswordConfirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panNewPassword)).EndInit();
            this.panNewPassword.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panCurrentPassword;
        private DevExpress.XtraEditors.LabelControl lblCurrentPassword;
        private DevExpress.XtraEditors.TextEdit txtCurrentPassword;
        private DevExpress.XtraEditors.PanelControl panMain;
        private DevExpress.XtraEditors.PanelControl panButton;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panNewPassword;
        private DevExpress.XtraEditors.TextEdit txtNewPassword;
        private DevExpress.XtraEditors.LabelControl lblNewPassword;
        private DevExpress.XtraEditors.PanelControl panNewPasswordConfirm;
        private DevExpress.XtraEditors.TextEdit txtNewPasswordConfirm;
        private DevExpress.XtraEditors.LabelControl lblNewPasswordConfirm;
    }
}