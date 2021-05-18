namespace Micube.SmartMES.SystemManagement
{
    partial class ToolbarAuthority
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.treeMenu = new Micube.Framework.SmartControls.SmartTreeList();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdUserClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMenuToolbar = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartBandedGrid2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartTreeList1 = new Micube.Framework.SmartControls.SmartTreeList();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartTreeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(296, 511);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(692, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(692, 506);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1002, 540);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartGroupBox1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartSpliterContainer2);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(692, 506);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
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
            this.smartGroupBox1.Size = new System.Drawing.Size(300, 506);
            this.smartGroupBox1.TabIndex = 1;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // treeMenu
            // 
            this.treeMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeMenu.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeMenu.DisplayMember = null;
            this.treeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenu.LabelText = null;
            this.treeMenu.LanguageKey = null;
            this.treeMenu.Location = new System.Drawing.Point(2, 27);
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
            this.treeMenu.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeMenu.OptionsFind.FindNullPrompt = "";
            this.treeMenu.OptionsFind.ShowCloseButton = false;
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
            this.treeMenu.Size = new System.Drawing.Size(296, 477);
            this.treeMenu.TabIndex = 4;
            this.treeMenu.ValueMember = "ID";
            this.treeMenu.ValueNodeTypeFieldName = "Equipment";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdUserClass);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdMenuToolbar);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(382, 506);
            this.smartSpliterContainer2.SplitterPosition = 350;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdUserClass
            // 
            this.grdUserClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUserClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserClass.IsUsePaging = false;
            this.grdUserClass.LanguageKey = "GRIDUSERCLASSLIST";
            this.grdUserClass.Location = new System.Drawing.Point(0, 0);
            this.grdUserClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserClass.Name = "grdUserClass";
            this.grdUserClass.ShowBorder = true;
            this.grdUserClass.Size = new System.Drawing.Size(382, 350);
            this.grdUserClass.TabIndex = 5;
            // 
            // grdMenuToolbar
            // 
            this.grdMenuToolbar.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMenuToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMenuToolbar.IsUsePaging = false;
            this.grdMenuToolbar.LanguageKey = "GRIDTOOLBARAUTHORITYLIST";
            this.grdMenuToolbar.Location = new System.Drawing.Point(0, 0);
            this.grdMenuToolbar.Margin = new System.Windows.Forms.Padding(0);
            this.grdMenuToolbar.Name = "grdMenuToolbar";
            this.grdMenuToolbar.ShowBorder = true;
            this.grdMenuToolbar.Size = new System.Drawing.Size(382, 146);
            this.grdMenuToolbar.TabIndex = 0;
            // 
            // smartBandedGrid2
            // 
            this.smartBandedGrid2.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.smartBandedGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartBandedGrid2.IsUsePaging = false;
            this.smartBandedGrid2.LanguageKey = null;
            this.smartBandedGrid2.Location = new System.Drawing.Point(2, 31);
            this.smartBandedGrid2.Margin = new System.Windows.Forms.Padding(0);
            this.smartBandedGrid2.Name = "smartBandedGrid2";
            this.smartBandedGrid2.ShowBorder = true;
            this.smartBandedGrid2.Size = new System.Drawing.Size(388, 257);
            this.smartBandedGrid2.TabIndex = 0;
            // 
            // smartTreeList1
            // 
            this.smartTreeList1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartTreeList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.smartTreeList1.DisplayMember = null;
            this.smartTreeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTreeList1.LabelText = null;
            this.smartTreeList1.LanguageKey = null;
            this.smartTreeList1.Location = new System.Drawing.Point(2, 31);
            this.smartTreeList1.MaxHeight = 0;
            this.smartTreeList1.Name = "smartTreeList1";
            this.smartTreeList1.NodeTypeFieldName = "NODETYPE";
            this.smartTreeList1.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.smartTreeList1.OptionsBehavior.AutoPopulateColumns = false;
            this.smartTreeList1.OptionsBehavior.Editable = false;
            this.smartTreeList1.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.smartTreeList1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.smartTreeList1.OptionsFilter.AllowMRUFilterList = false;
            this.smartTreeList1.OptionsFind.AlwaysVisible = true;
            this.smartTreeList1.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.smartTreeList1.OptionsFind.FindNullPrompt = "";
            this.smartTreeList1.OptionsFind.ShowCloseButton = false;
            this.smartTreeList1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.smartTreeList1.OptionsView.ShowColumns = false;
            this.smartTreeList1.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.smartTreeList1.OptionsView.ShowHorzLines = false;
            this.smartTreeList1.OptionsView.ShowIndentAsRowStyle = true;
            this.smartTreeList1.OptionsView.ShowIndicator = false;
            this.smartTreeList1.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.smartTreeList1.OptionsView.ShowVertLines = false;
            this.smartTreeList1.ParentMember = "ParentID";
            this.smartTreeList1.ResultIsLeafLevel = true;
            this.smartTreeList1.Size = new System.Drawing.Size(296, 478);
            this.smartTreeList1.TabIndex = 4;
            this.smartTreeList1.ValueMember = "ID";
            this.smartTreeList1.ValueNodeTypeFieldName = "Equipment";
            // 
            // ToolbarAuthority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 560);
            this.Name = "ToolbarAuthority";
            this.Text = "ToolbarAuthority";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartTreeList1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartTreeList treeMenu;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdUserClass;
        private Framework.SmartControls.SmartBandedGrid grdMenuToolbar;
        private Framework.SmartControls.SmartBandedGrid smartBandedGrid2;
        private Framework.SmartControls.SmartTreeList smartTreeList1;
    }
}