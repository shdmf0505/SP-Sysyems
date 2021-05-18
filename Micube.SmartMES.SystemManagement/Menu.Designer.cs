namespace Micube.SmartMES.SystemManagement
{
    partial class Menu
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
            this.grdMenu = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.treeMenu = new Micube.Framework.SmartControls.SmartTreeList();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            // 
            // grdMenu
            // 
            this.grdMenu.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMenu.IsUsePaging = false;
            this.grdMenu.LanguageKey = "GRIDMENULIST";
            this.grdMenu.Location = new System.Drawing.Point(0, 0);
            this.grdMenu.Margin = new System.Windows.Forms.Padding(0);
            this.grdMenu.Name = "grdMenu";
            this.grdMenu.ShowBorder = true;
            this.grdMenu.Size = new System.Drawing.Size(160, 396);
            this.grdMenu.TabIndex = 1;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartGroupBox1);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdMenu);
            this.smartSpliterContainer1.Size = new System.Drawing.Size(470, 396);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 1;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.treeMenu);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "TREEMENULIST";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(300, 396);
            this.smartGroupBox1.TabIndex = 2;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // treeMenu
            // 
            this.treeMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeMenu.DisplayMember = null;
            this.treeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenu.LabelText = null;
            this.treeMenu.LanguageKey = null;
            this.treeMenu.Location = new System.Drawing.Point(2, 27);
            this.treeMenu.Margin = new System.Windows.Forms.Padding(0);
            this.treeMenu.MaxHeight = 0;
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.NodeTypeFieldName = "NODETYPE";
            this.treeMenu.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeMenu.OptionsBehavior.AutoPopulateColumns = false;
            this.treeMenu.OptionsBehavior.Editable = false;
            this.treeMenu.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.treeMenu.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treeMenu.OptionsFilter.AllowMRUFilterList = false;
            this.treeMenu.OptionsFind.AlwaysVisible = true;
            this.treeMenu.OptionsFind.ClearFindOnClose = false;
            this.treeMenu.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeMenu.OptionsFind.FindNullPrompt = "";
            this.treeMenu.OptionsFind.ShowClearButton = false;
            this.treeMenu.OptionsFind.ShowCloseButton = false;
            this.treeMenu.OptionsFind.ShowFindButton = false;
            this.treeMenu.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeMenu.OptionsView.ShowColumns = false;
            this.treeMenu.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeMenu.OptionsView.ShowHorzLines = false;
            this.treeMenu.OptionsView.ShowIndentAsRowStyle = true;
            this.treeMenu.OptionsView.ShowIndicator = false;
            this.treeMenu.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeMenu.OptionsView.ShowVertLines = false;
            this.treeMenu.ParentMember = "ParentID";
            this.treeMenu.ResultIsLeafLevel = true;
            this.treeMenu.Size = new System.Drawing.Size(296, 367);
            this.treeMenu.TabIndex = 0;
            this.treeMenu.ValueMember = "ID";
            this.treeMenu.ValueNodeTypeFieldName = "Equipment";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Menu";
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdMenu;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartTreeList treeMenu;
    }
}