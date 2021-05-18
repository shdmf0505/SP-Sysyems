namespace Micube.SmartMES.StandardInfo
{
    partial class SparePartClassManagement
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
            this.grpBox = new Micube.Framework.SmartControls.SmartGroupBox();
            this.treeSparePartClass = new Micube.Framework.SmartControls.SmartTreeList();
            this.grdSparePartClass = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpBox)).BeginInit();
            this.grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSparePartClass)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grpBox);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdSparePartClass);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(475, 401);
            this.smartSpliterContainer1.SplitterPosition = 300;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.treeSparePartClass);
            this.grpBox.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBox.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpBox.LanguageKey = "SPAREPARTCLASSTREE";
            this.grpBox.Location = new System.Drawing.Point(0, 0);
            this.grpBox.Name = "grpBox";
            this.grpBox.ShowBorder = true;
            this.grpBox.Size = new System.Drawing.Size(300, 401);
            this.grpBox.TabIndex = 0;
            this.grpBox.Text = "smartGroupBox1";
            // 
            // treeSparePartClass
            // 
            this.treeSparePartClass.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeSparePartClass.DisplayMember = null;
            this.treeSparePartClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSparePartClass.LabelText = null;
            this.treeSparePartClass.LanguageKey = "SparePart";
            this.treeSparePartClass.Location = new System.Drawing.Point(2, 31);
            this.treeSparePartClass.MaxHeight = 0;
            this.treeSparePartClass.Name = "treeSparePartClass";
            this.treeSparePartClass.NodeTypeFieldName = "NODETYPE";
            this.treeSparePartClass.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeSparePartClass.OptionsBehavior.AutoPopulateColumns = false;
            this.treeSparePartClass.OptionsBehavior.Editable = false;
            this.treeSparePartClass.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.treeSparePartClass.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treeSparePartClass.OptionsFilter.AllowMRUFilterList = false;
            this.treeSparePartClass.OptionsFind.AlwaysVisible = true;
            this.treeSparePartClass.OptionsFind.ClearFindOnClose = false;
            this.treeSparePartClass.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.FindClick;
            this.treeSparePartClass.OptionsFind.FindNullPrompt = "";
            this.treeSparePartClass.OptionsFind.ShowClearButton = false;
            this.treeSparePartClass.OptionsFind.ShowCloseButton = false;
            this.treeSparePartClass.OptionsFind.ShowFindButton = false;
            this.treeSparePartClass.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeSparePartClass.OptionsView.ShowColumns = false;
            this.treeSparePartClass.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeSparePartClass.OptionsView.ShowHorzLines = false;
            this.treeSparePartClass.OptionsView.ShowIndentAsRowStyle = true;
            this.treeSparePartClass.OptionsView.ShowIndicator = false;
            this.treeSparePartClass.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeSparePartClass.OptionsView.ShowVertLines = false;
            this.treeSparePartClass.ParentMember = "ParentID";
            this.treeSparePartClass.ResultIsLeafLevel = true;
            this.treeSparePartClass.Size = new System.Drawing.Size(296, 368);
            this.treeSparePartClass.TabIndex = 0;
            this.treeSparePartClass.ValueMember = "ID";
            this.treeSparePartClass.ValueNodeTypeFieldName = "Equipment";
            // 
            // grdSparePartClass
            // 
            this.grdSparePartClass.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSparePartClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSparePartClass.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSparePartClass.IsUsePaging = false;
            this.grdSparePartClass.LanguageKey = "SPAREPARTCLASSLIST";
            this.grdSparePartClass.Location = new System.Drawing.Point(0, 0);
            this.grdSparePartClass.Margin = new System.Windows.Forms.Padding(0);
            this.grdSparePartClass.Name = "grdSparePartClass";
            this.grdSparePartClass.ShowBorder = true;
            this.grdSparePartClass.ShowStatusBar = false;
            this.grdSparePartClass.Size = new System.Drawing.Size(170, 401);
            this.grdSparePartClass.TabIndex = 0;
            // 
            // SparePartClassManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "SparePartClassManagement";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBox)).EndInit();
            this.grpBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeSparePartClass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox grpBox;
        private Framework.SmartControls.SmartTreeList treeSparePartClass;
        private Framework.SmartControls.SmartBandedGrid grdSparePartClass;
    }
}