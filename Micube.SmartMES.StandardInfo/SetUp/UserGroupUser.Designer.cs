namespace Micube.SmartMES.StandardInfo
{
    partial class UserGroupUser
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
            this.grdUserGroup = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdUserGroupUser = new Micube.Framework.SmartControls.SmartBandedGrid();
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
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 520);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(693, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(693, 524);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(998, 553);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnUserSearch);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(646, 23);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnUserSearch
            // 
            this.btnUserSearch.AllowFocus = false;
            this.btnUserSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUserSearch.IsBusy = false;
            this.btnUserSearch.IsWrite = false;
            this.btnUserSearch.LanguageKey = "SearchUser";
            this.btnUserSearch.Location = new System.Drawing.Point(558, 0);
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
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdUserGroup);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdUserGroupUser);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(693, 524);
            this.smartSpliterContainer1.SplitterPosition = 400;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdUserGroup
            // 
            this.grdUserGroup.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUserGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserGroup.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Refresh;
            this.grdUserGroup.IsUsePaging = false;
            this.grdUserGroup.LanguageKey = "GRIDUSERCLASSLIST";
            this.grdUserGroup.Location = new System.Drawing.Point(0, 0);
            this.grdUserGroup.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserGroup.Name = "grdUserGroup";
            this.grdUserGroup.ShowBorder = true;
            this.grdUserGroup.Size = new System.Drawing.Size(400, 524);
            this.grdUserGroup.TabIndex = 2;
            // 
            // grdUserGroupUser
            // 
            this.grdUserGroupUser.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUserGroupUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserGroupUser.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdUserGroupUser.IsUsePaging = false;
            this.grdUserGroupUser.LanguageKey = "GRIDUSERCLASSUSERLIST";
            this.grdUserGroupUser.Location = new System.Drawing.Point(0, 0);
            this.grdUserGroupUser.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserGroupUser.Name = "grdUserGroupUser";
            this.grdUserGroupUser.ShowBorder = true;
            this.grdUserGroupUser.Size = new System.Drawing.Size(288, 524);
            this.grdUserGroupUser.TabIndex = 3;
            // 
            // UserGroupUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 573);
            this.Name = "UserGroupUser";
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
        private Framework.SmartControls.SmartBandedGrid grdUserGroup;
        private Framework.SmartControls.SmartBandedGrid grdUserGroupUser;
    }
}