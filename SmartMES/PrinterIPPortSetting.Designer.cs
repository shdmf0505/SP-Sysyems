namespace SmartMES
{
    partial class PrinterIPPortSetting
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
            this.txtIPAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblIPAddress = new DevExpress.XtraEditors.LabelControl();
            this.panMain = new DevExpress.XtraEditors.PanelControl();
            this.panButton = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.panNewPassword = new DevExpress.XtraEditors.PanelControl();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.lblPort = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panCurrentPassword)).BeginInit();
            this.panCurrentPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panNewPassword)).BeginInit();
            this.panNewPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panCurrentPassword
            // 
            this.panCurrentPassword.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panCurrentPassword.Controls.Add(this.txtIPAddress);
            this.panCurrentPassword.Controls.Add(this.lblIPAddress);
            this.panCurrentPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.panCurrentPassword.Location = new System.Drawing.Point(10, 10);
            this.panCurrentPassword.Margin = new System.Windows.Forms.Padding(0);
            this.panCurrentPassword.Name = "panCurrentPassword";
            this.panCurrentPassword.Size = new System.Drawing.Size(280, 26);
            this.panCurrentPassword.TabIndex = 0;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(126, 2);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIPAddress.Properties.Appearance.Options.UseFont = true;
            this.txtIPAddress.Properties.AutoHeight = false;
            this.txtIPAddress.Size = new System.Drawing.Size(150, 22);
            this.txtIPAddress.TabIndex = 1;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblIPAddress.Appearance.Options.UseFont = true;
            this.lblIPAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblIPAddress.Location = new System.Drawing.Point(5, 2);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(120, 22);
            this.lblIPAddress.TabIndex = 0;
            this.lblIPAddress.Text = "IP 주소";
            // 
            // panMain
            // 
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.Controls.Add(this.panButton);
            this.panMain.Controls.Add(this.panNewPassword);
            this.panMain.Controls.Add(this.panCurrentPassword);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Margin = new System.Windows.Forms.Padding(0);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(10);
            this.panMain.Size = new System.Drawing.Size(300, 102);
            this.panMain.TabIndex = 1;
            // 
            // panButton
            // 
            this.panButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panButton.Controls.Add(this.btnCancel);
            this.panButton.Controls.Add(this.btnConfirm);
            this.panButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panButton.Location = new System.Drawing.Point(10, 62);
            this.panButton.Margin = new System.Windows.Forms.Padding(0);
            this.panButton.Name = "panButton";
            this.panButton.Size = new System.Drawing.Size(280, 30);
            this.panButton.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(201, 4);
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
            this.btnConfirm.Location = new System.Drawing.Point(116, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "확인";
            // 
            // panNewPassword
            // 
            this.panNewPassword.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panNewPassword.Controls.Add(this.txtPort);
            this.panNewPassword.Controls.Add(this.lblPort);
            this.panNewPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.panNewPassword.Location = new System.Drawing.Point(10, 36);
            this.panNewPassword.Margin = new System.Windows.Forms.Padding(0);
            this.panNewPassword.Name = "panNewPassword";
            this.panNewPassword.Size = new System.Drawing.Size(280, 26);
            this.panNewPassword.TabIndex = 2;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(126, 2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPort.Properties.Appearance.Options.UseFont = true;
            this.txtPort.Properties.AutoHeight = false;
            this.txtPort.Size = new System.Drawing.Size(150, 22);
            this.txtPort.TabIndex = 3;
            // 
            // lblPort
            // 
            this.lblPort.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblPort.Appearance.Options.UseFont = true;
            this.lblPort.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPort.Location = new System.Drawing.Point(5, 2);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(120, 22);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port";
            // 
            // PrinterIPPortSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(300, 102);
            this.Controls.Add(this.panMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrinterIPPortSetting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IP / Port 설정";
            ((System.ComponentModel.ISupportInitialize)(this.panCurrentPassword)).EndInit();
            this.panCurrentPassword.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panNewPassword)).EndInit();
            this.panNewPassword.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panCurrentPassword;
        private DevExpress.XtraEditors.LabelControl lblIPAddress;
        private DevExpress.XtraEditors.TextEdit txtIPAddress;
        private DevExpress.XtraEditors.PanelControl panMain;
        private DevExpress.XtraEditors.PanelControl panButton;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panNewPassword;
        private DevExpress.XtraEditors.TextEdit txtPort;
        private DevExpress.XtraEditors.LabelControl lblPort;
    }
}