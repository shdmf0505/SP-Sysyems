namespace Micube.SmartMES.SystemManagement
{
    partial class UserClassUser
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUserSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdUserClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdUserClassUser = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(296, 524);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(412, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(412, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(722, 553);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnUserSearch);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(45, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(367, 23);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnUserSearch
            // 
            this.btnUserSearch.AllowFocus = false;
            this.btnUserSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUserSearch.IsBusy = false;
            this.btnUserSearch.LanguageKey = "SearchUser";
            this.btnUserSearch.Location = new System.Drawing.Point(279, 0);
            this.btnUserSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnUserSearch.Name = "btnUserSearch";
            this.btnUserSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnUserSearch.Size = new System.Drawing.Size(85, 23);
            this.btnUserSearch.TabIndex = 4;
            this.btnUserSearch.Text = "사용자 조회";
            this.btnUserSearch.TooltipLanguageKey = "SEARCHUSER";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdUserClass);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdUserClassUser);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(412, 519);
            this.smartSpliterContainer1.SplitterPosition = 400;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdUserClass
            // 
            this.grdUserClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUserClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserClass.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Refresh;
            this.grdUserClass.IsUsePaging = false;
            this.grdUserClass.LanguageKey = "GRIDUSERCLASSLIST";
            this.grdUserClass.Location = new System.Drawing.Point(0, 0);
            this.grdUserClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserClass.Name = "grdUserClass";
            this.grdUserClass.ShowBorder = true;
            this.grdUserClass.Size = new System.Drawing.Size(400, 519);
            this.grdUserClass.TabIndex = 2;
            // 
            // grdUserClassUser
            // 
            this.grdUserClassUser.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUserClassUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserClassUser.IsUsePaging = false;
            this.grdUserClassUser.LanguageKey = "GRIDUSERCLASSUSERLIST";
            this.grdUserClassUser.Location = new System.Drawing.Point(0, 0);
            this.grdUserClassUser.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserClassUser.Name = "grdUserClassUser";
            this.grdUserClassUser.ShowBorder = true;
            this.grdUserClassUser.Size = new System.Drawing.Size(2, 519);
            this.grdUserClassUser.TabIndex = 3;
            // 
            // UserClassUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 573);
            this.Name = "UserClassUser";
            this.Text = "UserClassUser";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnUserSearch;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdUserClass;
        private Framework.SmartControls.SmartBandedGrid grdUserClassUser;
    }
}