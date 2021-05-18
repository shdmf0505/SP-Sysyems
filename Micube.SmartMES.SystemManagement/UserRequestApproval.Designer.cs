namespace Micube.SmartMES.SystemManagement
{
    partial class UserRequestApproval
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
            this.grdUser = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUseApproval = new Micube.Framework.SmartControls.SmartButton();
            this.btnInitPassword = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.tableLayoutPanel1);
            this.pnlToolbar.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdUser);
            // 
            // grdUser
            // 
            this.grdUser.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUser.IsUsePaging = false;
            this.grdUser.LanguageKey = "USERLIST";
            this.grdUser.Location = new System.Drawing.Point(0, 0);
            this.grdUser.Margin = new System.Windows.Forms.Padding(0);
            this.grdUser.Name = "grdUser";
            this.grdUser.ShowBorder = true;
            this.grdUser.Size = new System.Drawing.Size(470, 396);
            this.grdUser.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.Controls.Add(this.btnInitPassword, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUseApproval, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(109, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(361, 23);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnUseApproval
            // 
            this.btnUseApproval.AllowFocus = false;
            this.btnUseApproval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUseApproval.IsBusy = false;
            this.btnUseApproval.LanguageKey = "USEAPPROVAL";
            this.btnUseApproval.Location = new System.Drawing.Point(172, 0);
            this.btnUseApproval.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnUseApproval.Name = "btnUseApproval";
            this.btnUseApproval.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnUseApproval.Size = new System.Drawing.Size(75, 23);
            this.btnUseApproval.TabIndex = 0;
            this.btnUseApproval.Text = "사용승인";
            this.btnUseApproval.TooltipLanguageKey = "";
            // 
            // btnInitPassword
            // 
            this.btnInitPassword.AllowFocus = false;
            this.btnInitPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInitPassword.IsBusy = false;
            this.btnInitPassword.LanguageKey = "INITPASSWORD";
            this.btnInitPassword.Location = new System.Drawing.Point(253, 0);
            this.btnInitPassword.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInitPassword.Name = "btnInitPassword";
            this.btnInitPassword.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInitPassword.Size = new System.Drawing.Size(105, 23);
            this.btnInitPassword.TabIndex = 1;
            this.btnInitPassword.Text = "비밀번호 초기화";
            this.btnInitPassword.TooltipLanguageKey = "";
            // 
            // UserRequestApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "UserRequestApproval";
            this.Text = "User";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnUseApproval;
        private Framework.SmartControls.SmartButton btnInitPassword;
    }
}