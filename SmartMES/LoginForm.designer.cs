namespace SmartMES
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.linkForgot = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.linkRequest = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.picClose = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.chkIDSave = new Micube.Framework.SmartControls.SmartCheckBox();
            this.btnMESLogin = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPlantId = new Micube.Framework.SmartControls.SmartComboBox();
            this.cboLanguageType = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtID = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtPW = new Micube.Framework.SmartControls.SmartTextBox();
            this.cboService = new Micube.Framework.SmartControls.SmartComboBox();
            this.picLogo = new Micube.Framework.SmartControls.SmartPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIDSave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLanguageType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPW.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboService.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // linkForgot
            // 
            this.linkForgot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkForgot.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.linkForgot.Appearance.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.linkForgot.Appearance.Options.UseForeColor = true;
            this.linkForgot.Appearance.Options.UseLinkColor = true;
            this.linkForgot.Appearance.Options.UseTextOptions = true;
            this.linkForgot.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.linkForgot.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.linkForgot.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.linkForgot.Location = new System.Drawing.Point(256, 386);
            this.linkForgot.Name = "linkForgot";
            this.linkForgot.Size = new System.Drawing.Size(290, 14);
            this.linkForgot.TabIndex = 34;
            // 
            // linkRequest
            // 
            this.linkRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkRequest.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.linkRequest.Appearance.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.linkRequest.Appearance.Options.UseForeColor = true;
            this.linkRequest.Appearance.Options.UseLinkColor = true;
            this.linkRequest.Appearance.Options.UseTextOptions = true;
            this.linkRequest.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.linkRequest.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.linkRequest.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.linkRequest.Location = new System.Drawing.Point(256, 364);
            this.linkRequest.Name = "linkRequest";
            this.linkRequest.Size = new System.Drawing.Size(290, 14);
            this.linkRequest.TabIndex = 33;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Appearance.BackColor = System.Drawing.Color.White;
            this.smartLabel2.Appearance.Options.UseBackColor = true;
            this.smartLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartLabel2.Location = new System.Drawing.Point(60, 410);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(530, 1);
            this.smartLabel2.TabIndex = 45;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.BackColor = System.Drawing.Color.White;
            this.smartLabel1.Appearance.Options.UseBackColor = true;
            this.smartLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartLabel1.Location = new System.Drawing.Point(60, 9);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(498, 1);
            this.smartLabel1.TabIndex = 44;
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.BackgroundImage = global::SmartMES.Properties.Resources.loginClose;
            this.picClose.EditValue = global::SmartMES.Properties.Resources.loginClose;
            this.picClose.Location = new System.Drawing.Point(558, 9);
            this.picClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picClose.Name = "picClose";
            this.picClose.Properties.AllowFocused = false;
            this.picClose.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(171)))), ((int)(((byte)(52)))));
            this.picClose.Properties.Appearance.Options.UseBackColor = true;
            this.picClose.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picClose.Properties.ShowMenu = false;
            this.picClose.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            this.picClose.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picClose.Size = new System.Drawing.Size(32, 28);
            this.picClose.TabIndex = 28;
            // 
            // chkIDSave
            // 
            this.chkIDSave.LanguageKey = "DI00006";
            this.chkIDSave.Location = new System.Drawing.Point(326, 285);
            this.chkIDSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIDSave.Name = "chkIDSave";
            this.chkIDSave.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.chkIDSave.Properties.Appearance.Options.UseForeColor = true;
            this.chkIDSave.Properties.Appearance.Options.UseTextOptions = true;
            this.chkIDSave.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkIDSave.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.chkIDSave.Properties.AutoHeight = false;
            this.chkIDSave.Properties.Caption = "";
            this.chkIDSave.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkIDSave.Size = new System.Drawing.Size(18, 17);
            this.chkIDSave.TabIndex = 30;
            // 
            // btnMESLogin
            // 
            this.btnMESLogin.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnMESLogin.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnMESLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMESLogin.Location = new System.Drawing.Point(326, 238);
            this.btnMESLogin.Name = "btnMESLogin";
            this.btnMESLogin.Size = new System.Drawing.Size(220, 31);
            this.btnMESLogin.TabIndex = 29;
            this.btnMESLogin.Click += new System.EventHandler(this.BtnMESLogin_Click);
            // 
            // cboPlantId
            // 
            this.cboPlantId.LabelText = null;
            this.cboPlantId.LanguageKey = null;
            this.cboPlantId.Location = new System.Drawing.Point(452, 318);
            this.cboPlantId.Margin = new System.Windows.Forms.Padding(0);
            this.cboPlantId.Name = "cboPlantId";
            this.cboPlantId.PopupWidth = 0;
            this.cboPlantId.Properties.AllowFocused = false;
            this.cboPlantId.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.cboPlantId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.cboPlantId.Properties.Appearance.Options.UseBackColor = true;
            this.cboPlantId.Properties.Appearance.Options.UseForeColor = true;
            this.cboPlantId.Properties.AutoHeight = false;
            this.cboPlantId.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cboPlantId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlantId.Properties.NullText = "";
            this.cboPlantId.Properties.PopupSizeable = false;
            this.cboPlantId.Properties.PopupWidthMode = DevExpress.XtraEditors.PopupWidthMode.UseEditorWidth;
            this.cboPlantId.Properties.ShowFooter = false;
            this.cboPlantId.Properties.ShowHeader = false;
            this.cboPlantId.ShowHeader = false;
            this.cboPlantId.Size = new System.Drawing.Size(94, 24);
            this.cboPlantId.TabIndex = 43;
            this.cboPlantId.VisibleColumns = null;
            this.cboPlantId.VisibleColumnsWidth = null;
            // 
            // cboLanguageType
            // 
            this.cboLanguageType.LabelText = null;
            this.cboLanguageType.LanguageKey = null;
            this.cboLanguageType.Location = new System.Drawing.Point(326, 318);
            this.cboLanguageType.Margin = new System.Windows.Forms.Padding(0);
            this.cboLanguageType.Name = "cboLanguageType";
            this.cboLanguageType.PopupWidth = 0;
            this.cboLanguageType.Properties.AllowFocused = false;
            this.cboLanguageType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.cboLanguageType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.cboLanguageType.Properties.Appearance.Options.UseBackColor = true;
            this.cboLanguageType.Properties.Appearance.Options.UseForeColor = true;
            this.cboLanguageType.Properties.AutoHeight = false;
            this.cboLanguageType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cboLanguageType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLanguageType.Properties.NullText = "";
            this.cboLanguageType.Properties.PopupSizeable = false;
            this.cboLanguageType.Properties.PopupWidthMode = DevExpress.XtraEditors.PopupWidthMode.UseEditorWidth;
            this.cboLanguageType.Properties.ShowFooter = false;
            this.cboLanguageType.Properties.ShowHeader = false;
            this.cboLanguageType.ShowHeader = false;
            this.cboLanguageType.Size = new System.Drawing.Size(94, 24);
            this.cboLanguageType.TabIndex = 42;
            this.cboLanguageType.VisibleColumns = null;
            this.cboLanguageType.VisibleColumnsWidth = null;
            // 
            // txtID
            // 
            this.txtID.LabelText = null;
            this.txtID.LanguageKey = null;
            this.txtID.Location = new System.Drawing.Point(326, 154);
            this.txtID.Margin = new System.Windows.Forms.Padding(0);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.txtID.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtID.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtID.Properties.Appearance.Options.UseBackColor = true;
            this.txtID.Properties.Appearance.Options.UseBorderColor = true;
            this.txtID.Properties.Appearance.Options.UseFont = true;
            this.txtID.Properties.Appearance.Options.UseForeColor = true;
            this.txtID.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(161)))), ((int)(((byte)(155)))));
            this.txtID.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtID.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.txtID.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtID.Properties.AppearanceFocused.Options.UseForeColor = true;
            this.txtID.Properties.AutoHeight = false;
            this.txtID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtID.Properties.NullText = "ID";
            this.txtID.Size = new System.Drawing.Size(220, 24);
            this.txtID.TabIndex = 0;
            // 
            // txtPW
            // 
            this.txtPW.LabelText = null;
            this.txtPW.LanguageKey = null;
            this.txtPW.Location = new System.Drawing.Point(326, 194);
            this.txtPW.Margin = new System.Windows.Forms.Padding(0);
            this.txtPW.Name = "txtPW";
            this.txtPW.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.txtPW.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtPW.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPW.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtPW.Properties.Appearance.Options.UseBackColor = true;
            this.txtPW.Properties.Appearance.Options.UseBorderColor = true;
            this.txtPW.Properties.Appearance.Options.UseFont = true;
            this.txtPW.Properties.Appearance.Options.UseForeColor = true;
            this.txtPW.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(161)))), ((int)(((byte)(155)))));
            this.txtPW.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPW.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(56)))), ((int)(((byte)(90)))));
            this.txtPW.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtPW.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtPW.Properties.AppearanceFocused.Options.UseForeColor = true;
            this.txtPW.Properties.AutoHeight = false;
            this.txtPW.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPW.Properties.UseSystemPasswordChar = true;
            this.txtPW.Size = new System.Drawing.Size(220, 24);
            this.txtPW.TabIndex = 1;
            // 
            // cboService
            // 
            this.cboService.LabelText = null;
            this.cboService.LanguageKey = null;
            this.cboService.Location = new System.Drawing.Point(446, 115);
            this.cboService.Name = "cboService";
            this.cboService.PopupWidth = 0;
            this.cboService.Properties.AllowFocused = false;
            this.cboService.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.cboService.Properties.Appearance.Options.UseBackColor = true;
            this.cboService.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cboService.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboService.Properties.NullText = "";
            this.cboService.Properties.ShowHeader = false;
            this.cboService.ShowHeader = false;
            this.cboService.Size = new System.Drawing.Size(100, 18);
            this.cboService.TabIndex = 47;
            this.cboService.Visible = false;
            this.cboService.VisibleColumns = null;
            this.cboService.VisibleColumnsWidth = null;
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(306, 46);
            this.picLogo.Name = "picLogo";
            this.picLogo.Properties.AllowFocused = false;
            this.picLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Properties.Appearance.Options.UseBackColor = true;
            this.picLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picLogo.Properties.ShowMenu = false;
            this.picLogo.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            this.picLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picLogo.Size = new System.Drawing.Size(240, 80);
            this.picLogo.TabIndex = 48;
            // 
            // LoginForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.Blue;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::SmartMES.Properties.Resources.LoginForm_New;
            this.ClientSize = new System.Drawing.Size(600, 420);
            this.ControlBox = false;
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.cboService);
            this.Controls.Add(this.smartLabel2);
            this.Controls.Add(this.smartLabel1);
            this.Controls.Add(this.linkRequest);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.chkIDSave);
            this.Controls.Add(this.linkForgot);
            this.Controls.Add(this.btnMESLogin);
            this.Controls.Add(this.cboPlantId);
            this.Controls.Add(this.cboLanguageType);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtPW);
            this.DoubleBuffered = true;
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 420);
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartLogin";
            this.TransparencyKey = System.Drawing.Color.Blue;
            this.Load += new System.EventHandler(this.SmartLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIDSave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLanguageType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPW.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboService.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Micube.Framework.SmartControls.SmartTextBox txtID;
        private Micube.Framework.SmartControls.SmartTextBox txtPW;
        private Micube.Framework.SmartControls.SmartComboBox cboLanguageType;
        private Micube.Framework.SmartControls.SmartComboBox cboPlantId;
        private Micube.Framework.SmartControls.SmartLabel btnMESLogin;
        private Micube.Framework.SmartControls.SmartCheckBox chkIDSave;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkForgot;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkRequest;
        private Micube.Framework.SmartControls.SmartPictureEdit picClose;
        private Micube.Framework.SmartControls.SmartLabel smartLabel1;
        private Micube.Framework.SmartControls.SmartLabel smartLabel2;
        private Micube.Framework.SmartControls.SmartComboBox cboService;
        private Micube.Framework.SmartControls.SmartPictureEdit picLogo;
    }
}